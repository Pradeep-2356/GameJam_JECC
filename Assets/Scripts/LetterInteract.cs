using UnityEngine;

public class LetterInteract : Interactable
{
    private Animator animator;
    private bool isOpened = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override void Interact()
    {
        if (isOpened) return;

        isOpened = true;
        animator.SetTrigger("OpenLetter");
    }
}
