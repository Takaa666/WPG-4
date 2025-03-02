using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Conditions
{
    [Category("Custom")]
    public class IfSeeTargetPlayAnimation : ConditionTask<Transform>
    {
        public BBParameter<Transform> target;
        public BBParameter<float> fieldOfView = 60f;
        public BBParameter<float> viewDistance = 10f;
        public BBParameter<string> animationName;
        private Animator animator;

        // Initialize Animator when the task starts
        protected override void OnEnable() 
        {
            animator = agent.GetComponent<Animator>();

            if (animator == null) 
            {
                Debug.LogError("Animator component not found on agent.");
            }
        }

        // Called every frame to check the condition
        protected override bool OnCheck()
        {
            if (agent == null || target.value == null)
            {
                Debug.LogError("Agent or target is null.");
                return false;
            }

            // Calculate the direction and angle to the target
            Vector3 directionToTarget = (target.value.position - agent.position).normalized;
            float angle = Vector3.Angle(agent.forward, directionToTarget);

            // Debugging log for angle and field of view
            Debug.Log($"Angle to target: {angle}, Field of View: {fieldOfView.value}");

            // Check if the target is within the agent's field of view
            if (angle < fieldOfView.value * 0.5f)
            {
                float distanceToTarget = Vector3.Distance(agent.position, target.value.position);

                // Debugging log for distance and view distance
                Debug.Log($"Distance to target: {distanceToTarget}, View Distance: {viewDistance.value}");

                // Check if the target is within the agent's view distance
                if (distanceToTarget < viewDistance.value)
                {
                    RaycastHit hit;
                    Debug.DrawRay(agent.position, directionToTarget * viewDistance.value, Color.red); // Visualize the ray in the Scene

                    // Use LayerMask to ensure only Player is hit
                    int layerMask = LayerMask.GetMask("Player");

                    // Check if there's a clear line of sight to the target using a raycast with layer mask
                    if (Physics.Raycast(agent.position, directionToTarget, out hit, viewDistance.value, layerMask))
                    {
                        // Check if the object hit by the raycast is the target
                        if (hit.transform == target.value)
                        {
                            Debug.Log("Player detected, playing animation...");

                            // Play the specified animation if the animator is available
                            if (animator != null && !string.IsNullOrEmpty(animationName.value))
                            {
                                animator.Play(animationName.value);
                                Debug.Log($"Playing animation: {animationName.value}");
                            }
                            else
                            {
                                Debug.LogError("Animator not found or animation name is empty.");
                            }

                            return true;
                        }
                        else
                        {
                            Debug.Log("Raycast hit an object, but it's not the target.");
                        }
                    }
                    else
                    {
                        Debug.Log("Raycast did not hit the target.");
                    }
                }
            }

            return false;
        }
    }
}
