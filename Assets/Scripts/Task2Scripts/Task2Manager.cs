using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Task2Manager : MonoBehaviour
{
    #region Singleton Implementation

    public static Task2Manager Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    
    public CircleManager circleManager;
    public LineManager lineManager;

    public List<GameObject> listOfSpawnedCircles = new List<GameObject>();
    public List<GameObject> listOfTouchedCircles = new List<GameObject>();

    public void InitialiseCirclesAndLR()
    {
        circleManager.SpawnCircles();
        lineManager.AddColliderToLineRenderer();
    }

    public void DestroyCircles()
    {
        Debug.Log("Destroying circles");
        foreach (GameObject gameObject in listOfTouchedCircles)
        {
            Destroy(gameObject);
        }
        listOfTouchedCircles.Clear();
    }

    public void Restart()
    {
        foreach (GameObject gameObject in listOfSpawnedCircles)
        {
            Destroy(gameObject);
        }

        listOfSpawnedCircles.Clear();
        circleManager.SpawnCircles();
    }
}