using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyHandCollision : MonoBehaviour
{
    // Event yang dipicu ketika tangan monster mengenai pemain
    public static event System.Action OnPlayerHit;

    private void OnTriggerEnter(Collider other)
    {
        // Memastikan bahwa yang terkena tabrakan adalah pemain
        if (other.CompareTag("Player"))
        {
            // Memicu event ketika tangan monster mengenai pemain
            OnPlayerHit?.Invoke();
        }
    }
}
