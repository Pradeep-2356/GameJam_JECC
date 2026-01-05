using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
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
        Debug.Log("Player Health: " + currentHealth);

        animator.SetTrigger("Hurt");

        Debug.Log("Player took damage: " + damage);


        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        animator.SetTrigger("Die");
        GetComponent<PlayerController>().enabled = false;
    }
}
