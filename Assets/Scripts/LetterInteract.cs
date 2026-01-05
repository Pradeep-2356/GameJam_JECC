using UnityEngine;

public class LetterInteract : MonoBehaviour
{
    [TextArea(5, 10)]
    public string letterContent;

    private bool isOpened = false;

    public void Interact()
    {
        if (isOpened) return;

        isOpened = true;

        UIManager.Instance.ShowLetter(letterContent);

        gameObject.SetActive(false);
    }
}
