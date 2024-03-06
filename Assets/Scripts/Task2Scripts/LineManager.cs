using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public LayerMask collisionLayer;

    public bool isDrawing = false;

    // Adding colliders to dynamic points of a line renderer
    public void AddColliderToLineRenderer()
    {
        BoxCollider2D collider = lineRenderer.gameObject.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(lineRenderer.startWidth, lineRenderer.endWidth);
    }

    // Update to keep on checking mouse/touch input
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            StartDrawing();
        }

        if (isDrawing && (Input.GetMouseButton(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)))
        {
            ContinueDrawing();

            // Check collision with images while continuing to draw
            CheckCollision();
        }

        if ((Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)) && isDrawing)
        {
            StopDrawing();
        }
    }

    void StartDrawing()
    {
        isDrawing = true;
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, GetMouseWorldPosition());
    }

    void ContinueDrawing()
    {
        int positionIndex = lineRenderer.positionCount;
        lineRenderer.positionCount = positionIndex + 1;
        lineRenderer.SetPosition(positionIndex, GetMouseWorldPosition());
    }

    void StopDrawing()
    {
        isDrawing = false;

        // Initialize the circles destroying logic
        Task2Manager.Instance.DestroyCircles();
    }

    void CheckCollision()
    {
        Vector3[] linePositions = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(linePositions);

        for (int i = 0; i < linePositions.Length - 1; i++)
        {
            Vector3 startPosition = linePositions[i];
            Vector3 endPosition = linePositions[i + 1];

            RaycastHit2D[] hits = Physics2D.LinecastAll(startPosition, endPosition, collisionLayer);

            foreach (RaycastHit2D hit in hits)
            {
                // Perform actions when collision detected
                if (!Task2Manager.Instance.listOfTouchedCircles.Contains(hit.collider.gameObject))
                {
                    Task2Manager.Instance.listOfTouchedCircles.Add(hit.collider.gameObject);
                    Task2Manager.Instance.listOfSpawnedCircles.Remove(hit.collider.gameObject);
                }

            }
        }
    }

    // Returns mouse position from screen space to world space
    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10; // Distance from the camera to the line
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}
