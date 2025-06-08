using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class PlayCutsceneRemaja : MonoBehaviour
{
    public PuzzleKeduaManager puzzleManager;
    public PlayableDirector playableDirector;
    public Image panelScreen;
    public GameObject panel;
    public Move playerMoveScript; // Script player
    public GameObject playerVirtualCamera; // Virtual Camera player
    public Transform cutsceneSpawnPoint; // Lokasi spawn untuk cutscene
    public Transform playerTransform;    // Transform player
    private bool hasStartedSequence = false;
    public Animator animator;

    public GameObject panelAtas;
    public GameObject panelBawah;
    void Update()
    {
        if (!hasStartedSequence && puzzleManager != null && puzzleManager.QuestObjectDestroyed())
        {
            if (playerMoveScript != null)
                playerMoveScript.enabled = false;
            hasStartedSequence = true;
            StartCoroutine(PlaySequence());
        }
    }

    private IEnumerator PlaySequence()
    {
        // Tunggu 5 detik
        yield return new WaitForSeconds(3f);
        animator.SetTrigger("idle");
        
        if (playerVirtualCamera != null)
            playerVirtualCamera.SetActive(false);
        // Entrance Animation panel
        panel.SetActive(true);
        panelScreen.enabled = true;
        UIAutoAnimation anim = panelScreen.GetComponent<UIAutoAnimation>();
        anim.EntranceAnimation();
        panelAtas.SetActive(true);
        panelBawah.SetActive(true);
        
        if (playerTransform != null && cutsceneSpawnPoint != null)
        {
            playerTransform.position = cutsceneSpawnPoint.position;
            playerTransform.rotation = cutsceneSpawnPoint.rotation; // agar arah player pas di cutscene
        }
        yield return new WaitForSeconds(2f);

        // Exit Animation panel
        anim.ExitAnimation();

        yield return new WaitForSeconds(2f);

        // Disable player Move + disable virtual camera
        

        // Play PlayableDirector
        if (playableDirector != null)
        {
            playableDirector.Play();

            // Tunggu sampai selesai
            while (playableDirector.state == PlayState.Playing)
            {
                yield return null;
            }
        }

        // PlayableDirector selesai ? enable Move + camera kembali
        if (playerMoveScript != null)
            playerMoveScript.enabled = true;

        if (playerVirtualCamera != null)
            playerVirtualCamera.SetActive(true);
        panelAtas.SetActive(false );
        panelBawah.SetActive(false);
        Debug.Log("Playable Director finished. Player restored.");
        panel.SetActive(false);
    }
}
