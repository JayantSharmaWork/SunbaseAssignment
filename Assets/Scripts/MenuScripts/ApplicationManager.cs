using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationManager : MonoBehaviour
{
    #region Singleton Implementation

    public static ApplicationManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    const string MENU = "menu";
    const string TASK1 = "task1";
    const string TASK2 = "task2";

    private void Start()
    {
        InitializeConfiguration();
    }

    public void InitializeConfiguration()
    {
        InitializeUI();
        // Any other configuration type data can also be added,
        // For example audio, server, etc.
    }

    public void InitializeUI()
    {
        MenuState menuState = FindAnyObjectByType<MenuState>();
        Task1State task1State = FindAnyObjectByType<Task1State>();
        Task2State task2State = FindAnyObjectByType<Task2State>();

        UIStateManager.Instance.RegisterState(MENU, menuState);
        UIStateManager.Instance.RegisterState(TASK1, task1State);
        UIStateManager.Instance.RegisterState(TASK2, task2State);

        // Switch to menu state initially
        SwitchToMenu();
    }

    public void SwitchToMenu()
    {
        UIStateManager.Instance.SwitchState(MENU);
    }

    public void SwitchToTask1()
    {
        UIStateManager.Instance.SwitchState(TASK1);
    }

    public void SwitchToTask2()
    {
        UIStateManager.Instance.SwitchState(TASK2);
    }
}