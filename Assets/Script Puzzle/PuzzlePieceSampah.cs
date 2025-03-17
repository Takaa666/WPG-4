using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePieceSampah : MonoBehaviour
{
    public Vector2 Posisi;
    public bool diAtasObj;
    public int ID;
    Transform SaveObj;
    public GameObject completeObject;
    void Start()
    {
        Posisi = transform.position;
        completeObject.SetActive(false);
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
            /*transform.SetParent(SaveObj);
            transform.localPosition = Vector3.zero;
            transform.localScale = new Vector2(0.75f, 0.75f);
            SaveObj.GetComponent<SpriteRenderer>().enabled = false;*/

            // Delete the GameObject
            Destroy(gameObject);
                completeObject.SetActive(true);
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
