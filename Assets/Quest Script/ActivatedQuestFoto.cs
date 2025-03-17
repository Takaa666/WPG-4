using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatedQuestFoto : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
        if (ReturnTo3D.questFotoAktif == true)
        {
            this.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
