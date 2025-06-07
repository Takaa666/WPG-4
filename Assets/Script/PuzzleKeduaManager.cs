using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityStandardAssets.Utility.TimedObjectActivator;

public class PuzzleKeduaManager : MonoBehaviour
{
    public GameObject[] list;
    //public GameObject pintu;
    public Text text;
    //public GameObject text2;
    public HingeJoint hinge;
    public GameObject[] questObject;
    public List<GameObject> textQuest;

    /*public GameObject quest1;
    public GameObject quest2;
    public GameObject canvasQuest1;
    public GameObject canvasQuest2;*/

    // Update is called once per frame
    void Update()
    {
        if (QuestObjectDestroyed())
        {
            if (text != null)
            {
                text.text = "Cari Tuas Pertama";
                //text.fontSize = 38;
            }
           
        }
        
        UIAutoAnimation anim = text.GetComponent<UIAutoAnimation>();

        if (AllElementsDestroyed())
        {
            if(text != null)
            {
                text.enabled = true;
            }
            //Destroy(pintu);
            
            hinge.GetComponent<HingeJoint>();
            JointLimits limits = hinge.limits;
            limits.min = -90;
            limits.max = 90;
            hinge.limits = limits;
            NextQuest();
            /*quest1.SetActive(false);
            canvasQuest1.SetActive(false);
            canvasQuest2.SetActive(true);
            quest2.SetActive(true);*/
        }

    }

    bool QuestObjectDestroyed()
    {
        foreach (var obj in questObject)
        {
            if (obj != null)
                return false;
        }
        return true;
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
    /*private IEnumerator WaitThenExit(UIAutoAnimation anim, float delay)
    {
        if (text.IsActive())
        {
            yield return new WaitForSeconds(delay);
            anim.ExitAnimation();
            //text.enabled = false;
            text2.SetActive(false);
        }
       
    }*/

    void NextQuest()
    {
        foreach (GameObject quest in textQuest)
        {
            quest.SetActive(false);
        }
    }

    
}
