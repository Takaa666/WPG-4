using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

[Category("Custom")]
public class MoveToTargetAndPlayAnimationWhenClose : ActionTask<Transform>
{
    public BBParameter<GameObject> target;
    public float moveSpeed = 3f;
    public float stoppingDistance = 1f;
    public float detectionRadius = 10f; // <- Radius deteksi area scanning

    public BBParameter<Animator> animator; 
    public string moveAnimationName = "Walk";   
    public string closeAnimationName = "Interact"; 

    private bool hasPlayedCloseAnimation = false;

    protected override void OnExecute()
    {
        hasPlayedCloseAnimation = false;

        // Cek apakah target terlalu jauh saat pertama kali eksekusi
        if (target.value == null || Vector3.Distance(agent.position, target.value.transform.position) > detectionRadius)
        {
            EndAction(false);
            return;
        }

        if (animator.value != null)
        {
            animator.value.Play(moveAnimationName);
        }
    }

    protected override void OnUpdate()
    {
        if (target.value == null)
        {
            EndAction(false);
            return;
        }

        float distance = Vector3.Distance(agent.position, target.value.transform.position);

        // Jika target keluar dari area deteksi, hentikan task
        if (distance > detectionRadius)
        {
            EndAction(false);
            return;
        }

        if (distance <= stoppingDistance)
        {
            if (!hasPlayedCloseAnimation)
            {
                if (animator.value != null)
                {
                    animator.value.Play(closeAnimationName);
                }
                hasPlayedCloseAnimation = true;
            }
            EndAction(true); 
            return;
        }

        Vector3 direction = (target.value.transform.position - agent.position).normalized;
        agent.position += direction * moveSpeed * Time.deltaTime;
    }
}
