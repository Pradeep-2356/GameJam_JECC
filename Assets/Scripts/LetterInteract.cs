using UnityEngine;

public class LetterInteract : MonoBehaviour
{
    private Animator animator;
    private bool isOpened = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Interact()
    {
        if (isOpened) return;

        isOpened = true;
        animator.SetTrigger("OpenLetter");
    }
}
