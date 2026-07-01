using UnityEngine;

public class Mob : MonoBehaviour, IInteractable
{
    private bool mobState = false;

    public void Interact()
    {
        mobState = !mobState;

        if (mobState)
            Debug.Log("State set to true");
        else
            Debug.Log("State set to false");
    }

    public string GetInteractPrompt()
    {
        if (mobState)
            return "Interact to make false";
        else
            return "Interact to make true";
    }
}