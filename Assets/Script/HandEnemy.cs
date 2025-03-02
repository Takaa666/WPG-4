using UnityEngine;

public class HandEnemy : MonoBehaviour
{
    [SerializeField] private Collider handCollider; // Referensi ke collider tangan
    [SerializeField] private Animator animator;     // Referensi ke Animator
    [SerializeField] private string attackAnimationName = "Attack"; // Nama animasi serangan

    private void Start()
    {
        if (handCollider != null)
        {
            handCollider.enabled = false; // Pastikan collider tidak aktif di awal
        }
    }

    private void Update()
    {
        // Memeriksa apakah animasi serangan sedang diputar
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(attackAnimationName))
        {
            ActivateCollider();
        }
        else
        {
            DeactivateCollider();
        }
    }

    private void ActivateCollider()
    {
        if (!handCollider.enabled)
        {
            handCollider.enabled = true;
        }
    }

    private void DeactivateCollider()
    {
        if (handCollider.enabled)
        {
            handCollider.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(100);  // Atur nilai damage sesuai kebutuhan
            }
        }
    }
}
