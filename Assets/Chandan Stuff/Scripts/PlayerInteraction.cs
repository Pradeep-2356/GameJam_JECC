using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactDistance = 3f;

    void Update()
    {
        if (DialogueSystem.IsDialogueActive)
            return;

        Vector3 origin = transform.position + Vector3.up * 0.8f;
        RaycastHit hit;

        if (Physics.Raycast(origin, transform.forward, out hit, interactDistance))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();

            if (interactable != null && Input.GetKeyDown(KeyCode.E))
            {
                interactable.Interact();
            }
        }
    }
}
