using UnityEngine;

public class QuestGiver : NPC
{
    private bool questTurnedIn = false;
    public string[] questDialogue;
    public string[] reminderDialogue;
    public string[] completionDialogue;

    private Quest quest;

    public override void Interact()
{
    // QUEST NOT YET ASSIGNED
    if (quest == null)
    {
        AssignQuest();
        DialogueSystem.Instance.AddNewDialogue(questDialogue, npcName);
        return;
    }

    // QUEST ASSIGNED BUT NOT COMPLETED
    if (!quest.Completed)
    {
        DialogueSystem.Instance.AddNewDialogue(reminderDialogue, npcName);
        return;
    }

    // QUEST COMPLETED BUT NOT TURNED IN
    if (quest.Completed && !questTurnedIn)
    {
        questTurnedIn = true;
        DialogueSystem.Instance.AddNewDialogue(completionDialogue, npcName);
        quest.GiveReward();
        return;
    }

    // QUEST ALREADY TURNED IN
    DialogueSystem.Instance.AddNewDialogue(
        new string[] { "Thank you again, hero." },
        npcName
    );
}


    void AssignQuest()
{
    quest = gameObject.AddComponent<Quest>();
    quest.QuestName = "Kill the Enemy";
    quest.Description = "Eliminate the threat nearby";

    quest.Goals.Add(new KillGoal(quest, 1, 1));
}

}
