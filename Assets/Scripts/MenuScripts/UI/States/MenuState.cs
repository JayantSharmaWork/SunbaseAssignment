using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : UIState
{
    public GameObject menuMainPanel;

    public override void Activate()
    {
        menuMainPanel.SetActive(true);
        // Extra logic could be added for other activation tasks
    }

    public override void Deactivate()
    {
        menuMainPanel.SetActive(false);
        // Extra logic could be added for other deactivation tasks
    }
}