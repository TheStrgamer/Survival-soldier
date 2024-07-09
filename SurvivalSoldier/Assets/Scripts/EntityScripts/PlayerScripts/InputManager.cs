using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    [SerializeField] private List<InputActionSerializable> inputActionEntries = new List<InputActionSerializable>();
    [SerializeField] private Dictionary<string, KeyCode> inputActions = new Dictionary<string, KeyCode>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Update()
    {
        foreach (var entry in inputActionEntries)
        {
            if (entry.actionName != null && !inputActions.ContainsKey(entry.actionName))
            {
                AddInputAction(entry.actionName, entry.key);
            }
        }
        
    }

    public static void AddInputAction(string actionName, KeyCode key)
    {
        if (!Instance.inputActions.ContainsKey(actionName))
        {
            Instance.inputActions[actionName] = key;
        }
    }
    
    public static KeyCode GetKeyCode(string actionName)
    {
        if (Instance.inputActions.ContainsKey(actionName))
        {
            return Instance.inputActions[actionName];
        }
        else
        {
            Debug.LogError("Input action " + actionName + " not found");
            return KeyCode.None;
        }
    }
}
