using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideMonster : MonoBehaviour
{
    public GameObject targetObject;
    public GameObject targetObject2;
    public GameObject monster;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(targetObject == null)
        {
            monster.SetActive(true);
        }
        if(targetObject && targetObject2 == null)
        {
            monster.SetActive(false);
        }
    }
}
