using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    public GameObject PopUp;
    private GameObject currrentTask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            if (currrentTask == null)
            {
                currrentTask = Instantiate(PopUp, transform);
            }
        }
    }
}
