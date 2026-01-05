using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 50;
    public int currentHealth;

    private Animator animator;
    private bool isDead;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        Debug.Log("Enemy Health: " + currentHealth);

        animator.SetTrigger("Hurt");

        Debug.Log("Enemy took damage: " + damage);


        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        animator.SetTrigger("Die");
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        GetComponent<EnemyAI>().enabled = false;
        Destroy(gameObject, 3f);
    }
}
