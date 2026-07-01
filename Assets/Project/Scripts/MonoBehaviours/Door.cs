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
            Debug.Log("CLOSED");
    }

    public string GetInteractPrompt()
    {
        if (doorState)
            return "Interact to close the door";
        else
            return "Interact to open the door";
    }
}