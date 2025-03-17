using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions
{
    [Category("Custom")]
    public class WaitWithAnimation : ActionTask
    {
        public BBParameter<float> waitTime = 1f;
        public BBParameter<string> animationName;
        public BBParameter<int> currentTaskStep;
        public int stepToComplete;
        private Animator animator;
        private float startTime;

        protected override void OnExecute()
        {
            animator = agent.GetComponent<Animator>();
            if (animator != null && !string.IsNullOrEmpty(animationName.value))
            {
                animator.Play(animationName.value);
            }
            startTime = Time.time;
        }

        protected override void OnUpdate()
        {
            if (Time.time - startTime >= waitTime.value)
            {
                currentTaskStep.value = stepToComplete;
                EndAction(true);
            }
        }
    }
}
