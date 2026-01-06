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

    // Stop other animations
    animator.ResetTrigger("Hurt");
    animator.SetBool("IsDead", true);

    // Lock player controller
    PlayerController controller = GetComponent<PlayerController>();
    if (controller != null)
        controller.enabled = false;

    // Stop physics movement
    Rigidbody rb = GetComponent<Rigidbody>();
    if (rb != null)
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;
    }

    // Optional: unlock cursor if needed
    // Cursor.lockState = CursorLockMode.None;
    // Cursor.visible = true;

     Destroy(gameObject, 1f);
    Debug.Log("PLAYER DEAD - CONTROLS LOCKED");
      GameManager.Instance.GameOver();
}


}
