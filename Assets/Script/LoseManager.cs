using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class LoseManager : MonoBehaviour
{
    public Collider collider1;
    public Collider collider2;
    public Collider collider3;
    public Collider collider4;
    public GameObject cameraMonster;
    public PlayableDirector playableDirector;
    public Move moveScript;
    public GameObject loseUI;
    public Transform monsterSpawnPoint;
    public Transform playerSpawnPoint;

    public Transform monsterTransform;
    public Transform playerTransform;

    public Transform monsterTransformRemaja;
    public Transform playerTransformRemaja;

    public GameObject targetObject;
    public GameObject monsterRemaja;
    public Transform monsterRemajaTransform;
    public GameObject monster;
    void Start()
    {
        collider1.enabled = false;
        collider2.enabled = false;
        cameraMonster.SetActive(false);
        loseUI.SetActive(false);

        playableDirector.stopped += OnPlayableDirectorStopped;
    }

    public void EnemyAttack()
    {
        collider1.enabled = true;
        collider2.enabled = true;
        collider3.enabled = true;
        collider4.enabled = true;
    }

    public void PlayLoseCutscene()
    {
       
        collider1.enabled = false;
        collider2.enabled = false;
        collider3.enabled = false;
        collider4.enabled = false;
        cameraMonster.SetActive(true);
        playableDirector.Play();
        moveScript.enabled = false;
    }

    public void CheckQuestRemaja()
    {
        if (targetObject == null)
        {
            monster.SetActive(false);
            monsterRemaja.SetActive(true) ;
        }
    }

    private void OnPlayableDirectorStopped(PlayableDirector director)
    {
        if (director == playableDirector)
        {
            loseUI.SetActive(true);
        }
    }

    public void Retry()
    {
        if (targetObject == null)
        {
            // Jika quest sudah selesai ? pakai posisi Remaja
            playerTransform.position = playerTransformRemaja.position;
            playerTransform.rotation = playerTransformRemaja.rotation;

            monsterRemajaTransform.position = monsterTransformRemaja.position;
            monsterRemajaTransform.rotation = monsterTransformRemaja.rotation;

            // Optional: monsterRemaja.SetActive(true); monster.SetActive(false); ? jika ingin diulang
        }
        else
        {
            // Kalau quest belum selesai ? spawn biasa
            playerTransform.position = playerSpawnPoint.position;
            playerTransform.rotation = playerSpawnPoint.rotation;

            monsterTransform.position = monsterSpawnPoint.position;
            monsterTransform.rotation = monsterSpawnPoint.rotation;
        }

        moveScript.enabled = true;
        cameraMonster.SetActive(false);
        loseUI.SetActive(false);
    }

    private void Update()
    {
        CheckQuestRemaja();
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenuNew");
    }
}
