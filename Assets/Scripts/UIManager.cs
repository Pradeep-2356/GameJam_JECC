using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Letter UI")]
    public GameObject letterPanel;
    public TextMeshProUGUI letterText;

    private Animator animator;
    private bool isLetterOpen = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        animator = letterPanel.GetComponent<Animator>();
    }

    void Update()
    {
        if (!isLetterOpen) return;

        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape))
        {
            HideLetter();
        }
    }

    public void ShowLetter(string content)
    {
        if (isLetterOpen) return;

        isLetterOpen = true;

        letterText.text = content;

        // DO NOT enable/disable panel
        animator.ResetTrigger("Close");
        animator.SetTrigger("Open");

        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void HideLetter()
    {
        if (!isLetterOpen) return;

        isLetterOpen = false;

        animator.ResetTrigger("Open");
        animator.SetTrigger("Close");

        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
