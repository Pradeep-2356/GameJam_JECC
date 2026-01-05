using UnityEngine;

public class BlacksmithInteraction : MonoBehaviour
{
    private bool playerInRange;
    private PlayerWeapon playerWeapon;

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            DialogueManager.Instance.OpenDialogue();

            if (playerWeapon != null)
                playerWeapon.EquipSword();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            playerWeapon = other.GetComponent<PlayerWeapon>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            DialogueManager.Instance.CloseDialogue();
        }
    }
}
