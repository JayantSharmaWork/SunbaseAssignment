using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor.PackageManager;
using UnityEngine;
using Newtonsoft.Json;

public class Task1Manager : MonoBehaviour
{
    #region Singleton Implementation

    public static Task1Manager Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    private string task1APIEndpoint = "https://qa.sunbasedata.com/sunbase/portal/api/assignment.jsp?cmd=client_data";

    public RootObject rootObject = new RootObject();

    public List<ClientDataObject> listOfClientDataObjects = new List<ClientDataObject>();

    // Client Data Tiles references
    public GameObject clientDataTilePrefab;
    public GameObject clientDataTilesParent;
    public List<GameObject> listOfClientDataTiles = new List<GameObject>();

    public void InitiateWebRequest()
    {
        // Clear local data before intialization
        rootObject = new RootObject();
        listOfClientDataObjects.Clear();

        // GET request
        APIManager.Instance.Get(task1APIEndpoint, OnSuccess, OnError);
    }

    private void OnSuccess(string response)
    {
        Debug.Log("Success! Response: " + response);
        ParseGetData(response);
    }

    private void OnError(string error)
    {
        Debug.LogError("Error: " + error);
    }

    public void ParseGetData(string response)
    {
        try
        {
            rootObject = JsonConvert.DeserializeObject<RootObject>(response);
        }
        catch (Exception e)
        {
            Debug.LogError("Error parsing JSON: " + e.Message);
            return;
        }

        CreateClientDataSerializableList();
    }

    public void CreateClientDataSerializableList()
    {
        for (int i = 0; i < rootObject.clients.Length - 1; i++)
        {
            string id = rootObject.clients[i].id.ToString();

            Data data = new Data();

            if (rootObject.data.TryGetValue(id, out data))
            {
                ClientDataObject clientDataObject = new ClientDataObject();
                clientDataObject.id = rootObject.clients[i].id;
                clientDataObject.label = rootObject.clients[i].label;
                clientDataObject.isManager = rootObject.clients[i].isManager;
                clientDataObject.name = data.name;
                clientDataObject.address = data.address;
                clientDataObject.points = data.points;
                listOfClientDataObjects.Add(clientDataObject);
            }
            else
            {

            }
        }
    }

    public void CreateClientTilesUI()
    {
        foreach (ClientDataObject clientDataObject in listOfClientDataObjects)
        {
            GameObject clientDataTile = Instantiate(clientDataTilePrefab, clientDataTilesParent.transform);
            ClientDetailsTileManager clientDetailsTileManager = clientDataTile.GetComponent<ClientDetailsTileManager>();
            Debug.Log("clientDetailsTileManager: " + clientDetailsTileManager);
            Debug.Log("clientDetailsTileManager.clientDataObject: " + clientDetailsTileManager.clientDataObject);
            clientDetailsTileManager.clientDataObject = clientDataObject;
            clientDetailsTileManager.SetTileDetails();
            listOfClientDataTiles.Add(clientDataTile);
        }
    }
}

[Serializable]
public class Client
{
    public bool isManager;
    public int id;
    public string label;
}

[Serializable]
public class Data
{
    public string address;
    public string name;
    public int points;
}

[Serializable]
public class RootObject
{
    public Client[] clients;
    public Dictionary<string, Data> data;
    public string label;
}

[Serializable]
public class DictObject
{
    public string index;
    public string address;
    public string name;
    public int points;
}

[Serializable]
public class ClientDataObject
{
    public int id;
    public string name;
    public string address;
    public int points;
    public string label;
    public bool isManager;
}