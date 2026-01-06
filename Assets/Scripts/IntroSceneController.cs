using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class IntroPressToContinue : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Start()
    {
        if (videoPlayer != null)
            videoPlayer.Play();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LoadNextScene();
        }
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
