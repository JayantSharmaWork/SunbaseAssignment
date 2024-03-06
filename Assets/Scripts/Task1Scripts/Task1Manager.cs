using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using TMPro;

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
    public bool isAPILoaded = false;

    public RootObject rootObject = new RootObject();

    public List<ClientDataObject> listOfClientDataObjects = new List<ClientDataObject>();

    // Client Data Tiles references
    public GameObject clientDataTilePrefab;
    public GameObject clientDataTilesParent;
    public List<GameObject> listOfClientDataTiles = new List<GameObject>();

    // Filter
    public TMP_Dropdown filterDropdown;

    public enum ClientFilters
    {
        ALL,
        MANAGER,
        NON_MANAGER
    }

    public ClientFilters currentClientsFilter;

    public void InitiateWebRequest()
    {
        if (isAPILoaded)
            return;
        // Clear local data before intialization
        rootObject = new RootObject();
        listOfClientDataObjects.Clear();

        // GET request
        APIManager.Instance.Get(task1APIEndpoint, OnSuccess, OnError);
    }

    private void OnSuccess(string response)
    {
        isAPILoaded = true;
        Debug.Log("Success! Response: " + response);
        ApplicationManager.Instance.task1Button.interactable = true;
        ParseGetData(response);
    }

    private void OnError(string error)
    {
        isAPILoaded = false;
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
        }
    }

    public void ClearClientDataTiles()
    {
        foreach (Transform transform in clientDataTilesParent.transform)
            Destroy(transform.gameObject);

        listOfClientDataTiles.Clear();
    }

    public void CreateClientTilesUI()
    {
        foreach (ClientDataObject clientDataObject in listOfClientDataObjects)
        {
            GameObject clientDataTile = Instantiate(clientDataTilePrefab, clientDataTilesParent.transform);
            ClientDetailsTileManager clientDetailsTileManager = clientDataTile.GetComponent<ClientDetailsTileManager>();
            clientDetailsTileManager.clientDataObject = clientDataObject;
            clientDetailsTileManager.SetTileDetails();
            listOfClientDataTiles.Add(clientDataTile);
        }

        FilterClientsList();
    }

    public void FilterClientsList()
    {
        currentClientsFilter = (ClientFilters)filterDropdown.value;

        foreach (GameObject clientDataTile in listOfClientDataTiles)
        {
            switch (currentClientsFilter)
            {
                case ClientFilters.ALL:
                    {
                        clientDataTile.SetActive(true);
                        break;
                    }
                case ClientFilters.MANAGER:
                    {
                        if (clientDataTile.GetComponent<ClientDetailsTileManager>().clientDataObject.isManager)
                            clientDataTile.SetActive(true);
                        else
                            clientDataTile.SetActive(false);
                        break;
                    }
                case ClientFilters.NON_MANAGER:
                    {
                        if (!clientDataTile.GetComponent<ClientDetailsTileManager>().clientDataObject.isManager)
                            clientDataTile.SetActive(true);
                        else
                            clientDataTile.SetActive(false);
                        break;
                    }
            }
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