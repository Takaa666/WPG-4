using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleNotif : MonoBehaviour
{
    public GameObject notifPuzzle;
    // Start is called before the first frame update
    void Start()
    {
        notifPuzzle.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (GameObject.FindWithTag("Player"))
        {
            notifPuzzle.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        notifPuzzle?.SetActive(false);
    }
}
