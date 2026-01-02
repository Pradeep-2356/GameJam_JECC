using UnityEngine;

public class NPC : Interactable
{
    public string npcName;

    [TextArea]
    public string[] dialogue;

    public override void Interact()
    {
        DialogueSystem.Instance.AddNewDialogue(dialogue, npcName);
    }
}
