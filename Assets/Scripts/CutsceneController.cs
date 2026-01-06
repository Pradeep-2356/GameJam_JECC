using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CutsceneController : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Start()
    {
        if (videoPlayer != null)
            videoPlayer.loopPointReached += OnVideoEnd;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SkipCutscene();
        }
    }

    public void SkipCutscene()
    {
        if (videoPlayer != null)
            videoPlayer.Stop();

        LoadNextScene();
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        LoadNextScene();
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
