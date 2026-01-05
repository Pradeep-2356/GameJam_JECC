using UnityEngine;

public class LetterInteract : MonoBehaviour
{
    private bool isOpened = false;

    public void Interact()
    {
        if (isOpened) return;

        isOpened = true;
        UIManager.Instance.ShowLetter();

        gameObject.SetActive(false);
    }
}
