using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private StatesBlackboard statesBlackboard;
    private bool doorState = false;

    public void Interact()
    {
        if (!statesBlackboard.Get<bool>("has_key"))
            return;

        doorState = !doorState;

        if (doorState)
            Debug.Log("OPENED");
        else
            Debug.Log("CLOSED");
    }

    public string GetInteractPrompt()
    {
        if (!statesBlackboard.Get<bool>("has_key"))
            return "Its locked. Need the key.";

        if (doorState)
            return "Close";
        else
            return "Open";
    }
}