using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions
{
    [Category("Custom")]
    public class MoveToTagWithAnimation : ActionTask<Transform>
    {
        public BBParameter<string> targetTag;
        public BBParameter<float> speed = 5f;
        public BBParameter<float> time = 10;
        public BBParameter<string> animationName;
        public BBParameter<int> currentTaskStep;
        public BBParameter<GameObject> footstepSoundObject;  // GameObject taken from Blackboard that contains AudioSource
        public int stepToComplete;

        private Animator animator;
        private Transform target;
        private AudioSource footstepAudioSource;  // Reference to the AudioSource on the GameObject

        protected override void OnExecute()
        {
            animator = agent.GetComponent<Animator>();

            // Get the AudioSource component from the footstepSoundObject from Blackboard
            if (footstepSoundObject.value != null)
            {
                footstepAudioSource = footstepSoundObject.value.GetComponent<AudioSource>();
            }

            if (animator != null && !string.IsNullOrEmpty(animationName.value))
            {
                animator.Play(animationName.value);
            }

            // Play the footstep sound if AudioSource is assigned
            if (footstepAudioSource != null && !footstepAudioSource.isPlaying)
            {
                footstepAudioSource.loop = true;  // Loop the footstep sound
                footstepAudioSource.Play();
            }

            FindClosestTarget();
        }

        protected override void OnUpdate()
        {
            if (target == null)
            {
                StopFootsteps();
                EndAction(false);
                return;
            }

            agent.position = Vector3.MoveTowards(agent.position, target.position, speed.value * Time.deltaTime);

            Vector3 direction = (target.position - agent.position).normalized;
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                agent.rotation = Quaternion.Slerp(agent.rotation, lookRotation, Time.deltaTime * speed.value);
            }

            if (Vector3.Distance(agent.position, target.position) < 0.1f)
            {
                currentTaskStep.value = stepToComplete;
                StopFootsteps();
                EndAction(true);
            }

            if (time.value > 0)
            {
                time.value -= Time.deltaTime;
                if (time.value <= 0)
                {
                    StopFootsteps();
                    EndAction(false);
                }
            }
        }

        private void FindClosestTarget()
        {
            GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag.value);
            float closestDistance = Mathf.Infinity;
            Transform closestTarget = null;

            foreach (GameObject potentialTarget in targets)
            {
                float distance = Vector3.Distance(agent.position, potentialTarget.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = potentialTarget.transform;
                }
            }

            target = closestTarget;
        }

        protected override void OnStop()
        {
            StopFootsteps();  // Ensure footstep sound stops when the action is stopped
        }

        // Method to stop the footstep sound
        private void StopFootsteps()
        {
            if (footstepAudioSource != null && footstepAudioSource.isPlaying)
            {
                footstepAudioSource.Stop();
            }
        }
    }
}
