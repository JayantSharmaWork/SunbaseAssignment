using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleManager : MonoBehaviour
{
    // Circle Image Prefab
    public GameObject circlePrefab;

    // Circle image spawning parent
    public GameObject circleParent;
    
    // Custom number of circles to spawn
    public int numberOfCircles = 10;
  
    // Spawn the 10 random circles
    public void SpawnCircles()
    {
        foreach (GameObject gameObject in Task2Manager.Instance.listOfSpawnedCircles)
        {
            Destroy(gameObject);
        }

        Task2Manager.Instance.listOfSpawnedCircles.Clear();

        for (int i = 0; i < numberOfCircles; i++)
        {
            Vector3 randomPosition = OnScreen();
            GameObject circle = Instantiate(circlePrefab, circleParent.transform);
            circle.transform.position = randomPosition;
            Task2Manager.Instance.listOfSpawnedCircles.Add(circle);
        }
    }

    // Returns the random vector position between screen boundaries
    public Vector3 OnScreen()
    {
        ScreenBorders screenBorder = new ScreenBorders();
        screenBorder.CalculateScreenBorders();
        return new Vector3(Random.Range(screenBorder.bottomLeft.x, screenBorder.bottomRight.x), Random.Range(screenBorder.bottomLeft.y, screenBorder.topLeft.y - (0.5f * screenBorder.topLeft.y)),1f);
    }
}

public class ScreenBorders
{
    public Vector3 bottomLeft { get; private set; }
    public Vector3 topLeft { get; private set; }
    public Vector3 bottomRight { get; private set; }
    public Vector3 topRight { get; private set; }
    public float width { get; private set; }
    public float height { get; private set; }

    public ScreenBorders()
    {
        CalculateScreenBorders();
    }

    public void CalculateScreenBorders()
    {
        // Get the camera used to render the scene
        Camera mainCamera = Camera.main;

        // Get the distance between the camera and the near clipping plane
        float cameraDistance = mainCamera.nearClipPlane;

        // Calculate the coordinates of the screen borders
        bottomLeft = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, cameraDistance));
        topLeft = mainCamera.ScreenToWorldPoint(new Vector3(0, Screen.height, cameraDistance));
        topRight = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cameraDistance));
        bottomRight = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0, cameraDistance));

        width = bottomRight.x - bottomLeft.x;
        height = topLeft.y - bottomLeft.y;
    }
}