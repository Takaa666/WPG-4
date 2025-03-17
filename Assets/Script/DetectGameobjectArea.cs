using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

[Category("Custom Conditions")]
public class DetectGameObjectInArea : ConditionTask<Transform>
{
    public Animator animator;
    public string animationToPlay;
    public string areaTag; // Tag untuk area collider yang kita cari
    private bool isInsideArea = false;

    protected override string OnInit()
    {
        return null;
    }

    protected override bool OnCheck()
    {
        // Mengecek apakah object di dalam area
        return isInsideArea;
    }

    // Ketika object masuk ke trigger area
    protected override void OnEnable()
    {
        base.OnEnable();
        isInsideArea = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(areaTag))
        {
            isInsideArea = true;
            PlayAnimation();
        }
    }

    // Ketika object keluar dari trigger area
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(areaTag))
        {
            isInsideArea = false;
            StopAnimation();
        }
    }

    // Memainkan animasi
    private void PlayAnimation()
    {
        if (animator != null && !string.IsNullOrEmpty(animationToPlay))
        {
            animator.Play(animationToPlay);
        }
    }

    // Menghentikan animasi
    private void StopAnimation()
    {
        if (animator != null && !string.IsNullOrEmpty(animationToPlay))
        {
            animator.Play("Idle"); // Mengganti animasi ke idle atau animasi berhenti
        }
    }
}
