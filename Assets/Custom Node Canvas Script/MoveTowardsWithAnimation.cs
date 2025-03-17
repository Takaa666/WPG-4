using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using System.Collections.Generic;

namespace NodeCanvas.Tasks.Actions
{
    [Category("Custom")]
    public class MoveTowardsWithAnimation : ActionTask<Transform>
    {
        public BBParameter<List<Transform>> targets; // Reference to the list of targets from the Blackboard
        public BBParameter<float> speed = 5f;
        public BBParameter<string> animationName;
        public BBParameter<int> currentTaskStep;
        public BBParameter<GameObject> walkingSoundObject; // GameObject taken from Blackboard
        public int stepToComplete;

        private Animator animator;
        private AudioSource walkingAudioSource; // Reference to the AudioSource on the walkingSoundObject
        private int currentTargetIndex = 0;

        protected override void OnExecute()
        {
            animator = agent.GetComponent<Animator>();

            // Get the AudioSource component from the walkingSoundObject from Blackboard
            if (walkingSoundObject.value != null)
            {
                walkingAudioSource = walkingSoundObject.value.GetComponent<AudioSource>();
            }

            if (animator != null && !string.IsNullOrEmpty(animationName.value))
            {
                animator.Play(animationName.value);
            }

            // Play the walking sound if AudioSource is assigned
            if (walkingAudioSource != null && !walkingAudioSource.isPlaying)
            {
                walkingAudioSource.loop = true; // Loop the sound while walking
                walkingAudioSource.Play();
            }
        }

        protected override void OnUpdate()
        {
            if (agent == null || targets.value == null || targets.value.Count == 0)
            {
                StopWalkingSound(); // Stop the sound if there are no targets
                EndAction(false);
                return;
            }

            Transform currentTarget = targets.value[currentTargetIndex];
            if (currentTarget == null)
            {
                StopWalkingSound(); // Stop the sound if target is null
                EndAction(false);
                return;
            }

            Vector3 direction = (currentTarget.position - agent.position).normalized;
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                agent.rotation = Quaternion.Slerp(agent.rotation, lookRotation, Time.deltaTime * speed.value);
            }

            agent.position = Vector3.MoveTowards(agent.position, currentTarget.position, speed.value * Time.deltaTime);

            // Stop the sound if the agent has reached the target
            if (Vector3.Distance(agent.position, currentTarget.position) < 0.1f)
            {
                currentTargetIndex++;
                if (currentTargetIndex >= targets.value.Count)
                {
                    currentTargetIndex = 0; // Reset to the first target
                    currentTaskStep.value = stepToComplete;
                    StopWalkingSound();
                    EndAction(true);
                }
            }
        }

        protected override void OnStop()
        {
            StopWalkingSound(); // Ensure sound stops when the action is stopped
        }

        // Method to stop the walking sound
        private void StopWalkingSound()
        {
            if (walkingAudioSource != null && walkingAudioSource.isPlaying)
            {
                walkingAudioSource.Stop();
            }
        }
    }
}
