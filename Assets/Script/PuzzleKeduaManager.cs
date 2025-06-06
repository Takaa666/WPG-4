using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleKeduaManager : MonoBehaviour
{
    public GameObject[] list;
    //public GameObject pintu;
    public Text text;
    public HingeJoint hinge;

    /*public GameObject quest1;
    public GameObject quest2;
    public GameObject canvasQuest1;
    public GameObject canvasQuest2;*/
    
    // Update is called once per frame
    void Update()
    {
        UIAutoAnimation anim = text.GetComponent<UIAutoAnimation>();

        if (AllElementsDestroyed())
        {
            if(text != null)
            {
                text.enabled = true;
            }
            //Destroy(pintu);
            if (anim != null && text != null)
            {
                anim.EntranceAnimation();
                StartCoroutine(WaitThenExit(anim, 3f)); // 3 detik delay
            }
            hinge.GetComponent<HingeJoint>();
            JointLimits limits = hinge.limits;
            limits.min = -90;
            limits.max = 90;
            hinge.limits = limits;
            /*quest1.SetActive(false);
            canvasQuest1.SetActive(false);
            canvasQuest2.SetActive(true);
            quest2.SetActive(true);*/
        }

    }

    bool AllElementsDestroyed()
    {
        foreach (var obj in list)
        {
            if (obj != null)
                return false;
        }
        return true;
    }
    private IEnumerator WaitThenExit(UIAutoAnimation anim, float delay)
    {
        yield return new WaitForSeconds(delay);
        anim.ExitAnimation();
        //text.enabled = false;
        Destroy(text);
    }
}
