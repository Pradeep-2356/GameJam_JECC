using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int damage = 10;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enemy hit something");

        if (other.CompareTag("Player"))
        {
            Debug.Log("Enemy hit PLAYER");

            PlayerHealth ph = other.GetComponent<PlayerHealth>();
            if (ph != null)
            {
                ph.TakeDamage(damage);
            }
        }
    }
}
