using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public GameObject sword;
    public bool hasSword;

    void Start()
    {
        sword.SetActive(false);
        hasSword = false;
    }

    public void EquipSword()
    {
        if (hasSword) return;

        sword.SetActive(true);
        hasSword = true;
    }
}
