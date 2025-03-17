using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpJournal : MonoBehaviour
{
    private bool journalActive = false;  // Mengecek status apakah journal sedang aktif atau tidak
    public GameObject journal;  // Referensi ke gameObject journal di dalam Unity

    // Start is called before the first frame update
    void Start()
    {
        // Pastikan journal dimulai dalam kondisi tidak aktif
        journal.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Ketika pemain menekan tombol Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Membalikkan status journalActive
            journalActive = !journalActive;

            // Mengaktifkan atau menonaktifkan journal berdasarkan status journalActive
            journal.SetActive(journalActive);
        }
    }
}
