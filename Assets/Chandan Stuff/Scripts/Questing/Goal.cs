using UnityEngine;

public class Goal
{
    public Quest Quest;
    public bool Completed;

    public virtual void Init() { }

    public void Complete()
    {
        Completed = true;
        Quest.CheckGoals();
    }
}

