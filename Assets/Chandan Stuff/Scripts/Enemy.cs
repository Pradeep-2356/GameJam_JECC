using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy
{
    public int enemyID = 1;

    public int ID => enemyID;

    void Update()
    {
        // TEMP: press K to kill enemy
        if (Input.GetKeyDown(KeyCode.K))
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Enemy killed: " + enemyID);
        CombatEvents.OnEnemyKilled?.Invoke(this);
        Destroy(gameObject);
    }
}
