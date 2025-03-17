using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static Timer Instance;
    public float timer;
    public Image tutorialMove;
    public Image tutorialPickUp;

    public Text tutorialMoving;
    public Text tutorialPick;
    //public GameObject tutorial2;
    // Start is called before the first frame update
    void Start()
    {
        tutorialMove.enabled =true;
        tutorialMoving.enabled =true;
        tutorialPickUp.enabled = false;
        tutorialPick.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 2.5)
        {
            tutorialMove.enabled = false;
            tutorialMoving.enabled = false;

            tutorialPickUp.enabled = true;
            tutorialPick.enabled = true;
        }

        if (timer <= 0f)
        {
            tutorialPickUp.enabled = false;
            tutorialPick.enabled = false;
            Destroy(this.gameObject);

        }
    }
}
