using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
public class CutsceneMonsterBangunManager : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public Image panelScreen;
    public GameObject panel;
    public Move playerMoveScript; // Script player
    public GameObject playerVirtualCamera; // Virtual Camera player
    public GameObject targetObjectQuest;
    private bool hasStartedSequence = false;
    public List<GameObject> virtualCamerasToDestroy; // LIST virtual camera yang akan dihancurkan

    // Start is called before the first frame update
    void Update()
    {

        if (targetObjectQuest == null && !hasStartedSequence)
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
        //animator.SetTrigger("idle");

        if (playerVirtualCamera != null)
            playerVirtualCamera.SetActive(false);
        // Entrance Animation panel
        panel.SetActive(true);
        panelScreen.enabled = true;
        UIAutoAnimation anim = panelScreen.GetComponent<UIAutoAnimation>();
        anim.EntranceAnimation();
        

        
        yield return new WaitForSeconds(2f);

        // Exit Animation panel
        anim.ExitAnimation();

        yield return new WaitForSeconds(2f);

        // Disable player Move + disable virtual camera


        // Play PlayableDirector
        if (playableDirector != null && targetObjectQuest == null)
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
        
        Debug.Log("Playable Director finished. Player restored.");
        foreach (GameObject cam in virtualCamerasToDestroy)
        {
            if (cam != null)
            {
                Destroy(cam);
                Debug.Log("Destroyed Virtual Camera: " + cam.name);
            }
        }
        panel.SetActive(false);
    }
}
