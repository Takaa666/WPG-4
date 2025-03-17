using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TotalItem : MonoBehaviour
{
    public static int totalItem = 0;
    //public GameObject minigameTrigger;
    //public MeshRenderer puzzleobject;
    //public Collider colider;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<Collider>().enabled = false;
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(totalItem == 4)
        {
            //this.gameObject.GetComponent<MeshRenderer>().enabled = true;
            this.gameObject.GetComponent<Collider>().enabled = true;
        }

        /*if (minigameTrigger.IsDestroyed())
        {
            this.gameObject.GetComponent <Collider>().enabled = true;
        }*/
        
        if (ReturnTo3D.triggerDestroy == true)
        {
            this.gameObject.GetComponent<Collider>().enabled = false;
        }

    }
}
