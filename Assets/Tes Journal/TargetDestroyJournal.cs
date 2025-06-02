using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDestroyJournal : MonoBehaviour
{
    public GameObject targetObject; // Target yang akan dihancurkan
    public GameObject image;        // UI Image yang akan diaktifkan
    public GameObject text;         // UI Text yang akan diaktifkan
    // Start is called before the first frame update
    void Start()
    {
        image.SetActive(false);
        text.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (targetObject == null)
        {
            image.SetActive(true);
            text.SetActive(true);
        }
    }
}
