using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task2State : UIState
{
    public GameObject task2MainPanel;

    public override void Activate()
    {
        task2MainPanel.SetActive(true);
        // Extra logic could be added for other activation tasks
    }

    public override void Deactivate()
    {
        task2MainPanel.SetActive(false);
        // Extra logic could be added for other deactivation tasks
    }
}