using UnityEngine;
using UnityEngine.UI;

public class ClearDataButton : MonoBehaviour
{
    public Button clearDataButton;

    private void Start()
    {
        if (clearDataButton != null)
        {
            clearDataButton.onClick.AddListener(OnClearDataButtonClick);
        }
    }

    private void OnClearDataButtonClick()
    {
        if (SceneStateManager.Instance != null)
        {
            SceneStateManager.Instance.ClearSceneData();
        }
    }
}
