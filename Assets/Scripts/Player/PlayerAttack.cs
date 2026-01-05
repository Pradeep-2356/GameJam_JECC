using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int baseDamage = 10;
    public float attackRange = 1.5f;
    public LayerMask enemyLayer;

    public void LightAttackHit()
    {
        DealDamage(1f);   // 1x damage
    }

    public void HeavyAttackHit()
    {
        DealDamage(1.5f); // or 2f if you want
    }

    void DealDamage(float multiplier)
    {
        Collider[] hits = Physics.OverlapSphere(
            transform.position + transform.forward,
            attackRange,
            enemyLayer
        );

        foreach (Collider hit in hits)
        {
            EnemyHealth enemy = hit.GetComponentInParent<EnemyHealth>();
            if (enemy != null)
            {
                int damage = Mathf.RoundToInt(baseDamage * multiplier);
                enemy.TakeDamage(damage);
                Debug.Log("Player attack damage called");

            }
        }
    }
}
