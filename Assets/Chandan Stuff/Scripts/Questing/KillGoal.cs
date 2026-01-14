using UnityEngine;

public class KillGoal : Goal
{
    private int targetEnemyID;
    private int requiredAmount;
    private int currentAmount;

    public KillGoal(Quest quest, int enemyID, int amount)
    {
        Quest = quest;
        targetEnemyID = enemyID;
        requiredAmount = amount;
        Init();
    }

    public override void Init()
    {
        CombatEvents.OnEnemyKilled += OnEnemyKilled;
    }

    void OnEnemyKilled(IEnemy enemy)
    {
        if (enemy.ID == targetEnemyID)
        {
            currentAmount++;
            Debug.Log($"Kill progress: {currentAmount}/{requiredAmount}");

            if (currentAmount >= requiredAmount)
            {
                Complete();
                CombatEvents.OnEnemyKilled -= OnEnemyKilled;
            }
        }
    }
}
