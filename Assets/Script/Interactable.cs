using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    public GameObject BendaInteractable;
    public GameObject PanelInteractable;
    public Collider boxCollider;

    void Start()
    {
    }

    void Update()
    {
        if (boxCollider.CompareTag("Benda Interactable"))
        {
            BendaInteractable.SetActive(true);
            PanelInteractable.SetActive(false);
            Debug.Log("Interactable");
        }
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("Benda Interactable"))
    //     {
    //         BendaInteractable.SetActive(false);
    //         PanelInteractable.SetActive(true);
    //         Debug.Log("Interactable");
    //     }
    // }

    // private void OnTriggerExit(Collider other)
    // {
    //     if (other.CompareTag("Benda Interactable"))
    //     {
    //         Debug.Log("Not Interactable");
    //     }
    // }
}
