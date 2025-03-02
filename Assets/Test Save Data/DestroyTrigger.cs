using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTrigger : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        if (ReturnTo3D.triggerDestroy == true)
        {
            this.gameObject.GetComponent<Collider>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
