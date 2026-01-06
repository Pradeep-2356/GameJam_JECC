using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject helpPanel;
    private Animator helpAnimator;
    private bool isHelpOpen;

    void Start()
    {
        helpAnimator = helpPanel.GetComponent<Animator>();
        helpPanel.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
        if (isHelpOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseHelp();
        }
    }

    public void PlayGame()
    {
        // Load next scene from MainMenu (CutScene)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OpenHelp()
    {
        helpPanel.SetActive(true);
        helpAnimator.ResetTrigger("Close");
        helpAnimator.SetTrigger("Open");
        isHelpOpen = true;
    }

    public void CloseHelp()
    {
        helpAnimator.ResetTrigger("Open");
        helpAnimator.SetTrigger("Close");
        isHelpOpen = false;

        Invoke(nameof(DisableHelpPanel), 0.3f);
    }

    void DisableHelpPanel()
    {
        helpPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }
}
