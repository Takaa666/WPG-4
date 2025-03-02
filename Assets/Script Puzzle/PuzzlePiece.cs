using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;


public class PuzzlePiece : MonoBehaviour
{
    public Vector2 Posisi;
    public bool diAtasObj;
    public int ID;
    Transform SaveObj;

    
    void Start()
    {
        Posisi = transform.position;
    }

    private void OnMouseDown()
    {

    }

    private void OnMouseUp()
    {
        if (diAtasObj)
        {
            int ID_TempatDrop = SaveObj.GetComponent<TempatDrop>().ID;

            if (ID == ID_TempatDrop)
            {
                
                Destroy(gameObject);
            }
            else
            {
                transform.position = Posisi;
            }
        }
        else
        {
            transform.position = Posisi;
        }
    }

    private void OnMouseDrag()
    {
        Vector2 Pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = Pos;
    }

    private void OnTriggerStay2D(Collider2D trig)
    {
        if (trig.gameObject.CompareTag("Drop"))
        {
            diAtasObj = true;
            SaveObj = trig.gameObject.transform;
        }
    }

    private void OnTriggerExit(Collider trig)
    {
        if (trig.gameObject.CompareTag("Drop"))
        {
            diAtasObj = false;
        }
    }

    
}
