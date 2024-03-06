using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClientDetailsTileManager : MonoBehaviour
{
    // Local object reference for each tile
    public ClientDataObject clientDataObject = new ClientDataObject();

    // UI public references
    public TextMeshProUGUI nameText;

    public void SetTileDetails()
    {
        nameText.text = clientDataObject.name;
    }
}