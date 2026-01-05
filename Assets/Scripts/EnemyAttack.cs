using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int damage = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Hit");
            // other.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }
}