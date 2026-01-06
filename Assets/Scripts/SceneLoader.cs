using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public GameObject loadingScreen;

    void Start()
    {
        loadingScreen.SetActive(false); // hide during normal gameplay
    }

    public void LoadNextScene()
    {
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        loadingScreen.SetActive(true); // show ONLY while loading

        AsyncOperation operation =
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

        operation.allowSceneActivation = false;

        while (operation.progress < 0.9f)
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.2f);
        operation.allowSceneActivation = true;

        yield return null; // wait 1 frame
        loadingScreen.SetActive(false); // hide after load
    }
}
