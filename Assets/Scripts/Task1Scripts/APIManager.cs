using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class APIManager : MonoBehaviour
{
    #region Singleton Implementation

    public static APIManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    // This script could cover all http calls to API or external links

    public void Get(string url, UnityAction<string> onSuccess, UnityAction<string> onError)
    {
        StartCoroutine(SendRequest(url, onSuccess, onError));
    }

    private IEnumerator SendRequest(string url, UnityAction<string> onSuccess, UnityAction<string> onError)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Received: " + webRequest.downloadHandler.text);
                onSuccess?.Invoke(webRequest.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Error: " + webRequest.error);
                onError?.Invoke("Response is empty.");
            }
        }
    }
}