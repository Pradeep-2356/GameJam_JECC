using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem Instance;

    public Text nameText;
    public Text dialogueText;

    private Queue<string> lines = new Queue<string>();

    void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    public void AddNewDialogue(string[] dialogue, string npcName)
    {
        gameObject.SetActive(true);
        nameText.text = npcName;
        lines.Clear();

        foreach (string line in dialogue)
            lines.Enqueue(line);

        DisplayNextLine();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextLine();
        }
    }

    void DisplayNextLine()
    {
        if (lines.Count == 0)
        {
            gameObject.SetActive(false);
            return;
        }

        dialogueText.text = lines.Dequeue();
    }
}
