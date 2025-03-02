using UnityEngine;

public class YourObjectScript : MonoBehaviour
{
    public string uniqueID;

    private void Awake()
    {
        if (string.IsNullOrEmpty(uniqueID))
        {
            uniqueID = System.Guid.NewGuid().ToString();
        }
    }

    private void OnDestroy()
    {
        if (SceneStateManager.Instance != null)
        {
            SceneStateManager.Instance.MarkObjectAsDestroyed(uniqueID);
        }
    }
}
