using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI; // Needed for NavMeshAgent

namespace NodeCanvas.Tasks.Conditions
{
    [Category("GameObject")]
    [Description("Chases the player when they enter the area and stops when they leave")]
    public class ChasePlayerOnEnterArea : ConditionTask<Transform>
    {
        [RequiredField]
        public BBParameter<GameObject> target;
        [Tooltip("The distance within which the enemy will detect the player.")]
        public BBParameter<float> chaseDistance = 20f;
        [Tooltip("The distance after which the enemy will stop chasing the player.")]
        public BBParameter<float> stopChaseDistance = 25f;
        [Tooltip("A layer mask to use for line of sight check.")]
        public BBParameter<LayerMask> layerMask = (LayerMask)(-1);
        [Tooltip("Name of the chase animation to play while chasing.")]
        public string chaseAnimation;
        [Tooltip("Name of the idle animation to play when not chasing.")]
        public string idleAnimation;
        public Vector3 offset;

        private RaycastHit hit;
        private Animator animator;
        private NavMeshAgent navMeshAgent;
        private bool isChasing = false;

        protected override string info {
            get { return "Chase Player if within range"; }
        }

        protected override void OnEnable() {
            // Get Animator and NavMeshAgent components from the agent (enemy)
            if (animator == null) {
                animator = agent.GetComponent<Animator>();
            }
            if (navMeshAgent == null) {
                navMeshAgent = agent.GetComponent<NavMeshAgent>();
            }
        }

        protected override bool OnCheck() {
            var t = target.value.transform;

            if (!t.gameObject.activeInHierarchy) {
                return false;
            }

            float distanceToPlayer = Vector3.Distance(agent.position, t.position);

            // Start chasing if within chase distance
            if (distanceToPlayer <= chaseDistance.value && IsInLineOfSight(t)) {
                StartChasing(t);
                return true;
            }

            // Stop chasing if player exits the stop chase distance
            if (isChasing && distanceToPlayer > stopChaseDistance.value) {
                StopChasing();
                return false;
            }

            return false;
        }

        private bool IsInLineOfSight(Transform t) {
            // Line of sight check using Physics.Linecast
            if (Physics.Linecast(agent.position + offset, t.position + offset, out hit, layerMask.value)) {
                if (hit.collider != t.GetComponent<Collider>()) {
                    return false;
                }
            }
            return true;
        }

        private void StartChasing(Transform player) {
            if (!isChasing) {
                isChasing = true;
                // Play the chase animation
                if (animator != null && !string.IsNullOrEmpty(chaseAnimation)) {
                    animator.Play(chaseAnimation);
                }
            }
            // Move towards the player using NavMeshAgent
            if (navMeshAgent != null) {
                navMeshAgent.SetDestination(player.position);
            }
        }

        private void StopChasing() {
            if (isChasing) {
                isChasing = false;
                // Play the idle animation
                if (animator != null && !string.IsNullOrEmpty(idleAnimation)) {
                    animator.Play(idleAnimation);
                }
            }
            // Stop movement
            if (navMeshAgent != null) {
                navMeshAgent.ResetPath();
            }
        }

        public override void OnDrawGizmosSelected() {
            if (agent != null) {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(agent.position, chaseDistance.value);
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(agent.position, stopChaseDistance.value);
            }
        }
    }
}
