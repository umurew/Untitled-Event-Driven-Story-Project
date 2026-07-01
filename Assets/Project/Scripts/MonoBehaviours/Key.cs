using UnityEngine;

public class Key : MonoBehaviour, IInteractable
{
    [SerializeField] private StatesBlackboard statesBlackboard;

    public void Interact()
    {
        Destroy(gameObject);
        statesBlackboard.Set("has_key", true);
    }

    public string GetInteractPrompt()
    {
        return "Take the key";
    }
}