using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatesBlackboard", menuName = "Scriptable Objects/StatesBlackboard")]
public class StatesBlackboard : ScriptableObject
{
    private Dictionary<string, object> StateDictionary = new();

    public void Set(string Key, object Value)
    {
        StateDictionary[Key] = Value;
        Debug.Log($"State set with the key \"{Key}\" to {Value}");
    }

    public T Get<T>(string Key)
    {
        if (StateDictionary.TryGetValue(Key, out object Value))
            return (T)Value;

        return default;
    }

    public void ResetStates() => StateDictionary.Clear();
}