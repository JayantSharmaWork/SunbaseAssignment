using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task1State : UIState
{
    public GameObject task1MainPanel;

    public override void Activate()
    {
        task1MainPanel.SetActive(true);
        // Extra logic could be added for other activation tasks

        //Task1Manager.Instance.InitiateWebRequest();
        Task1Manager.Instance.CreateClientTilesUI();
    }

    public override void Deactivate()
    {
        task1MainPanel.SetActive(false);
        // Extra logic could be added for other deactivation tasks
        Task1Manager.Instance.ClearClientDataTiles();
    }
}