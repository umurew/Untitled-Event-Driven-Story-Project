using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    private bool doorState = false;

    public void Interact()
    {
        doorState = !doorState;

        if (doorState)
            Debug.Log("OPENED");
        else
            Debug.LogError("CLOSED");
    }

    public string GetInteractPrompt()
    {
        return string.Empty;
    }
}