using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagBasedTrigger : MonoBehaviour
{
    public string allowedTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != allowedTag)
        {
            Debug.Log($"{other.name} tidak diizinkan masuk!");
            // Misalnya, teleport keluar
            other.transform.position -= other.transform.forward * 1f;
        }
    }
}
