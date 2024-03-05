using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStateManager : MonoBehaviour
{
    #region Singleton Implementation

    public static UIStateManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    // Dictionary for UI states
    private Dictionary<string, UIState> uiStates = new Dictionary<string, UIState>();

    // Current active state
    private UIState activeState;

    // Register a UI state
    public void RegisterState(string stateName, UIState state)
    {
        if (!uiStates.ContainsKey(stateName))
        {
            uiStates.Add(stateName, state);
        }
    }

    // Unregister to a specific UI state 
    public void UnregisterState(string stateName)
    {
        if (uiStates.ContainsKey(stateName))
        {
            uiStates.Remove(stateName);
        }
    }

    // Switch to a specific UI state 
    public void SwitchState(string stateName)
    {
        if (uiStates.TryGetValue(stateName, out UIState newState))
        {
            // Deactivate the current state
            if (activeState != null)
            {
                activeState.Deactivate();
            }

            // Activate the new state
            newState.Activate();
            activeState = newState;
        }
        else
        {
            Debug.LogError("UI state '" + stateName + "' not found!");
        }
    }

}