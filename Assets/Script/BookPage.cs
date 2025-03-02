using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BookPage : MonoBehaviour
{
    public GameObject target;
    public Image unlockedPage;
    // Start is called before the first frame update
    void Start()
    {
        unlockedPage.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(target.IsDestroyed())
        {
            unlockedPage.enabled = true;
        }
    }
}
