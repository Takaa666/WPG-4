using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class BackToMainWorld : MonoBehaviour
{
    public PlayableDirector playableDirector;

    void Start()
    {
        if (playableDirector != null)
        {
            // Subscribe ke event Stopped
            playableDirector.stopped += OnPlayableDirectorStopped;
        }
    }

    void OnDestroy()
    {
        if (playableDirector != null)
        {
            // Unsubscribe untuk mencegah memory leak
            playableDirector.stopped -= OnPlayableDirectorStopped;
        }
    }

    void OnPlayableDirectorStopped(PlayableDirector director)
    {
        Debug.Log("PlayableDirector selesai, keluar game.");

        // Keluar game
        Application.Quit();

        // Jika di editor Unity, untuk testing:
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
