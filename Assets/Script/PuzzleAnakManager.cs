using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleAnakManager : MonoBehaviour
{
    public GameObject targetPuzzle;
    public Image panel;

    [Header("Puzzle Background")]
    public GameObject puzzleBackground; // Tambahkan ini di Inspector
    [Header("Puzzle Images (3)")]
    //public RectTransform[] puzzlePieces;

    public GameObject puzzle;
    private bool hasStartedPuzzle = false;
    private bool isCheckingPuzzle = false;
    public HingeJoint hinge;

    void Start()
    {
        if (panel != null)
            panel.enabled = false;

        if (puzzleBackground != null)
            puzzleBackground.SetActive(false);
    }

    void Update()
    {
        if (targetPuzzle == null && !hasStartedPuzzle)
        {
            hasStartedPuzzle = true;

            // Tampilkan panel dan animasi masuk
            if (panel != null)
            {
                panel.enabled = true;

                var panelAnim = panel.GetComponent<UIAutoAnimation>();
                if (panelAnim != null)
                    panelAnim.EntranceAnimation();
            }

            // Tampilkan puzzle background dan animasi masuk
            if (puzzleBackground != null)
            {
                puzzleBackground.SetActive(true);

                var bgAnim = puzzleBackground.GetComponent<UIAutoAnimation>();
                if (bgAnim != null)
                    bgAnim.EntranceAnimation();
            }

            StartCoroutine(CheckPuzzleCompletion());

        }
    }

    private IEnumerator CheckPuzzleCompletion()
    {
        isCheckingPuzzle = true;

        while (isCheckingPuzzle)
        {
            bool allCorrect = true;

            if (puzzle.transform.childCount == 0)
            {
                isCheckingPuzzle = false;

                // Exit panel animation
                if (panel != null)
                {
                    var panelAnim = panel.GetComponent<UIAutoAnimation>();
                    if (panelAnim != null)
                        panelAnim.ExitAnimation();
                    //Destroy(panel);
                }

                // Exit background animation and deactivate
                if (puzzleBackground != null)
                {
                    var bgAnim = puzzleBackground.GetComponent<UIAutoAnimation>();
                    if (bgAnim != null)
                        bgAnim.ExitAnimation();

                    yield return new WaitForSeconds(1f); // Tunggu animasi selesai (jika perlu delay)
                    puzzleBackground.SetActive(false);
                }

                // Atur HingeJoint setelah puzzle selesai
                if (hinge != null)
                {
                    JointLimits limits = hinge.limits;
                    limits.min = -90;
                    limits.max = 90;
                    hinge.limits = limits;
                }

                yield break;
            }

            yield return new WaitForSeconds(0.5f);
        }
    }
}
