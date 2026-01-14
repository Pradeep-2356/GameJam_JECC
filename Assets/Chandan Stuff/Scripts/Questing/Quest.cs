using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public string QuestName;
    public string Description;

    public List<Goal> Goals = new List<Goal>();
    public bool Completed;

    public void CheckGoals()
{
    Completed = Goals.All(g => g.Completed);
    Debug.Log("Quest completed status: " + Completed);
}


    public void GiveReward()
    {
        Debug.Log("Quest completed: " + QuestName);
    }
}
