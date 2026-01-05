using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    public GameObject dialoguePanel;

    private void Awake()
    {
        Instance = this;
        dialoguePanel.SetActive(false);
    }

    public void OpenDialogue()
    {
        dialoguePanel.SetActive(true);
    }

    public void CloseDialogue()
    {
        dialoguePanel.SetActive(false);
    }
}
