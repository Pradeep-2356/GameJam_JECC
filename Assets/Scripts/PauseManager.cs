using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance;

    public GameObject pausePanel;

    [Header("Scenes")]
    public string mainMenuSceneName = "Main_Menu";

    private Animator animator;
    private bool isPaused = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        animator = pausePanel.GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
                Pause();
            else
                Resume();
        }
    }

    public void Pause()
    {
        if (isPaused) return;

        isPaused = true;
        pausePanel.SetActive(true);

        animator.ResetTrigger("Close");
        animator.SetTrigger("Open");

        Time.timeScale = 0f;

        LockPlayer(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // 🔘 RESUME BUTTON
    public void Resume()
    {
        if (!isPaused) return;

        isPaused = false;

        animator.ResetTrigger("Open");
        animator.SetTrigger("Close");

        Time.timeScale = 1f;

        LockPlayer(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    // 🔘 QUIT BUTTON
    public void Quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName);
    }

    void LockPlayer(bool lockPlayer)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (!player) return;

        PlayerController controller = player.GetComponent<PlayerController>();
        if (controller)
            controller.enabled = !lockPlayer;

        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb && lockPlayer)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
