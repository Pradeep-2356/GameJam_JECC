using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;

    private Animator animator;
    private bool isDead;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();

        healthBar.SetMaxHealth(maxHealth);
    }

   public void TakeDamage(int damage)
{
    if (isDead) return;

    currentHealth -= damage;
    currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

    healthBar.SetHealth(currentHealth);

    if (currentHealth <= 0)
    {
        Die();
        return; // 🔴 VERY IMPORTANT
    }

    animator.SetTrigger("Hurt");
}


  void Die()
{
    if (isDead) return;

    isDead = true;

    animator.ResetTrigger("Hurt"); // safety
    animator.SetBool("IsDead", true);

    GetComponent<PlayerController>().enabled = false;
}

}
