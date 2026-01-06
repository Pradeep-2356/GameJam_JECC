using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Win Condition")]
    public int demonsToKill = 20;
    public int demonsKilled = 0;

    [Header("Start Message")]
    public GameObject startMessageUI;
    public float messageDuration = 3f;

    [Header("Score UI")]
    public TextMeshProUGUI scoreText;

    [Header("Win UI")]
    public GameObject winTextUI;
    public float winDelay = 3f;

    [Header("Game Over UI")]
    public GameObject gameOverTextUI;
    public float gameOverDelay = 3f;

    public string mainMenuSceneName = "Main_Menu";

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        UpdateScoreUI();

        if (startMessageUI != null)
        {
            startMessageUI.SetActive(true);
            Invoke(nameof(HideStartMessage), messageDuration);
        }

        if (winTextUI != null) winTextUI.SetActive(false);
        if (gameOverTextUI != null) gameOverTextUI.SetActive(false);
    }

       void HideStartMessage()
    {
        startMessageUI.SetActive(false);
    }

    public void DemonKilled()
    {
        demonsKilled++;
        UpdateScoreUI();

        if (demonsKilled >= demonsToKill)
        {
            WinGame();
        }
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = demonsKilled.ToString();
    }

    void WinGame()
    {
        if (winTextUI != null)
            winTextUI.SetActive(true);

        LockPlayer();
        Invoke(nameof(LoadMainMenu), winDelay);
    }

    public void GameOver()
    {
        if (gameOverTextUI != null)
            gameOverTextUI.SetActive(true);

        LockPlayer();
        Invoke(nameof(LoadMainMenu), gameOverDelay);
    }

    void LockPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (!player) return;

        PlayerController controller = player.GetComponent<PlayerController>();
        if (controller) controller.enabled = false;

        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }
    }

    void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
