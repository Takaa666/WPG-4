using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

[Category("Custom")]
public class AttackPlayerTask : ActionTask
{
    public BBParameter<GameObject> player;
    public BBParameter<string> attackAnimationName;
    public BBParameter<float> waitTime;

    private Animator animator;
    private bool attackStarted;
    private bool attackFinished;
    private float time;

    protected override void OnExecute()
    {
        time = 0;
        attackStarted = false;
        attackFinished = false;
        animator = agent.GetComponent<Animator>();
        
        if (animator != null)
        {
            animator.SetTrigger(attackAnimationName.value);
            attackStarted = true;
        }
        else
        {
            Debug.LogError("Agent does not have an Animator component.");
            EndAction(false);
        }
    }

    protected override void OnUpdate()
    {
        // Jika animasi serangan sedang dimainkan, tetap lanjutkan
        if (attackStarted && !attackFinished)
        {
            time += Time.deltaTime;
            AnimatorStateInfo currentAnimatorState = animator.GetCurrentAnimatorStateInfo(0);

            // Cek jika animasi masih berjalan
            if (currentAnimatorState.IsName(attackAnimationName.value))
            {
                // Tetap menunggu hingga animasi selesai
                if (time >= currentAnimatorState.length)
                {
                    attackFinished = true;
                    EndAction(true);
                }
            }
            else if (attackFinished)
            {
                // Jika animasi sudah selesai, selesaikan task
                EndAction(true);
            }
        }
    }

    protected override void OnStop()
    {
        // Optional: Reset kondisi saat task dihentikan
        attackStarted = false;
        attackFinished = false;
    }
}
