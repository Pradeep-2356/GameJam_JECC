using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public Collider weaponCollider;

    void Awake()
    {
        weaponCollider.enabled = false;
    }

    // CALLED BY ANIMATION EVENT
    public void EnableAttack()
    {
        weaponCollider.enabled = true;
    }

    // CALLED BY ANIMATION EVENT
    public void DisableAttack()
    {
        weaponCollider.enabled = false;
    }
}
