using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PopUpPuzzle : MonoBehaviour
{
    public GameObject targetPuzzle;
    public Image panel;
    public GameObject popUp;

    private bool hasShownPopup = false;

    void Start()
    {
        if (panel != null)
        {
            panel.enabled = false;
        }
    }

    void Update()
    {
        // Cek jika targetPuzzle sudah hilang (destroyed) dan popup belum ditampilkan
        if (targetPuzzle == null && !hasShownPopup)
        {
            hasShownPopup = true;

            if (panel != null)
            {
                panel.enabled = true;

                UIAutoAnimation anim = panel.GetComponent<UIAutoAnimation>();
                if (anim != null)
                {
                    anim.EntranceAnimation();
                    StartCoroutine(WaitThenExit(anim, 3f)); // 3 detik delay
                }
            }

            if (popUp != null)
            {
                popUp.SetActive(true);
            }
        }
    }

    private IEnumerator WaitThenExit(UIAutoAnimation anim, float delay)
    {
        yield return new WaitForSeconds(delay);
        anim.ExitAnimation();
        if (popUp.transform.childCount > 0)
        {
            popUp.GetComponentInChildren<Text>().enabled = false;
        }
    }
}
