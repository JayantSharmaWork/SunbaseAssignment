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

        Task2Manager.Instance.InitialiseCirclesAndLR();
        Task2Manager.Instance.lineManager.lineRenderer.gameObject.SetActive(true);
    }

    public override void Deactivate()
    {
        task2MainPanel.SetActive(false);
        // Extra logic could be added for other deactivation tasks

        Task2Manager.Instance.lineManager.lineRenderer.gameObject.SetActive(false);

    }
}