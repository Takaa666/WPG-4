using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

[Category("Custom")]
public class MoveToTargetAndPlayAnimationWhenClose : ActionTask<Transform>
{
    public BBParameter<GameObject> target;
    public float moveSpeed = 3f;
    public float stoppingDistance = 1f;
    public float detectionRadius = 10f;

    public BBParameter<Animator> animator;
    public string moveAnimationName = "Walk";
    public string closeAnimationName = "Interact";

    public BBParameter<AudioSource> screamSound;
    public float screamInterval = 4f;

    public float attackAnimationDuration = 2f; // <- Tambahan: durasi animasi serangan

    private bool hasPlayedCloseAnimation = false;
    private bool isAttacking = false;
    private float attackTimer = 0f;

    private float screamTimer = 0f;

    protected override void OnExecute()
    {
        hasPlayedCloseAnimation = false;
        isAttacking = false;
        screamTimer = 0f;
        attackTimer = 0f;

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

        // Jika sedang menyerang, tunggu animasi selesai
        if (isAttacking)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackAnimationDuration)
            {
                EndAction(true); // Animasi serangan selesai
            }
            return;
        }

        // Jika target di luar radius sebelum menyerang, batalkan
        if (distance > detectionRadius && !hasPlayedCloseAnimation)
        {
            EndAction(false);
            return;
        }

        // Jika sudah cukup dekat, mulai serangan
        if (distance <= stoppingDistance)
        {
            if (!hasPlayedCloseAnimation)
            {
                if (animator.value != null)
                {
                    animator.value.Play(closeAnimationName);
                }

                hasPlayedCloseAnimation = true;
                isAttacking = true;
                attackTimer = 0f;
            }

            return; // Tunggu animasi serangan selesai
        }

        // Bergerak ke arah target
        // Bergerak ke arah target
        Vector3 direction = (target.value.transform.position - agent.position).normalized;

        // Tambahkan rotasi ke arah target
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        agent.rotation = Quaternion.Slerp(agent.rotation, targetRotation, Time.deltaTime * 5f);

        // Bergerak maju
        agent.position += direction * moveSpeed * Time.deltaTime;


        // Suara teriakan setiap interval
        screamTimer += Time.deltaTime;
        if (screamTimer >= screamInterval)
        {
            PlayScream();
            screamTimer = 0f;
        }
    }

    private void PlayScream()
    {
        if (screamSound.value != null && !screamSound.value.isPlaying)
        {
            screamSound.value.Play();
        }
    }

    protected override void OnStop()
    {
        if (screamSound.value != null && screamSound.value.isPlaying)
        {
            screamSound.value.Stop();
        }
    }
}
