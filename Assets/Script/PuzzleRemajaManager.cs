using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleRemajaManager : MonoBehaviour
{
    public GameObject targetPuzzle;
    public Image panel;

    [Header("Puzzle Background")]
    public GameObject puzzleBackground; // Tambahkan ini di Inspector
    [Header("Puzzle Images (3)")]
    public RectTransform[] puzzlePieces;

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

            foreach (var piece in puzzlePieces)
            {
                float zRot = piece.eulerAngles.z;

                // Konversi rotasi agar menjadi dalam rentang -180 sampai 180
                if (zRot > 180f)
                    zRot -= 360f;

                // Cek apakah rotasi Z berada dalam rentang -10 sampai 10 derajat
                if (zRot < -10f || zRot > 10f)
                {
                    allCorrect = false;
                    break;
                }
            }

            if (allCorrect)
            {
                isCheckingPuzzle = false;

                var panelAnim = panel.GetComponent<UIAutoAnimation>();
                if (panelAnim != null)
                    panelAnim.ExitAnimation();

                var bgAnim = puzzleBackground.GetComponent<UIAutoAnimation>();
                if (bgAnim != null)
                    bgAnim.ExitAnimation();

                hinge.GetComponent<HingeJoint>();
                JointLimits limits = hinge.limits;
                limits.min = -90;
                limits.max = 90;
                hinge.limits = limits;
            }

            yield return new WaitForSeconds(0.5f);
        }
    }
}
