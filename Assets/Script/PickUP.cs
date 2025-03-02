using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUP : MonoBehaviour
{
    public GameObject KeyPick;
    void Start()
    {
        KeyPick.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKey(KeyCode.E))
            {
                this.gameObject.SetActive(false);
                KeyPick.SetActive(true);
            }
        }


    }
}
