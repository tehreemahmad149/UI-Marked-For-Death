using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform playerHead;
    public Transform playerHand;
    private List<Vector3> points;
    public Camera mainCamera;
    public float lineThickness;
    public float threshold;

    void Start()
    {
        points = new List<Vector3>();
        lineRenderer.startWidth = lineThickness;
        lineRenderer.endWidth = lineThickness;
        lineRenderer.positionCount = 0;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = touch.position;
            touchPosition.z = 10f; // Adjust based on your camera's setup
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);

            if (touch.phase == TouchPhase.Began)
            {
                points.Clear();
                lineRenderer.positionCount = 0;
            }

            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                if (points.Count == 0 || Vector3.Distance(points[points.Count - 1], worldPosition) > 0.1f)
                {
                    points.Add(worldPosition);
                    lineRenderer.positionCount = points.Count;
                    lineRenderer.SetPosition(points.Count - 1, worldPosition);
                }
            }

            if (touch.phase == TouchPhase.Ended)
            {
                ProcessDrawing(points);
                DetermineAttackType(points[0]);
            }
        }
    }

    private void DetermineAttackType(Vector3 startPoint)
    {
        if (IsNearPosition(startPoint, playerHead.position))
        {
            HeadshotAttack();
        }
        else if (IsNearPosition(startPoint, playerHand.position))
        {
            HandAttack();
        }
    }

    private bool IsNearPosition(Vector3 point, Vector3 targetPosition)
    {
        return Vector3.Distance(point, targetPosition) < threshold;
    }

    private void HeadshotAttack()
    {
        Debug.Log("Headshot!");
        // Implement headshot attack logic
    }

    private void HandAttack()
    {
        Debug.Log("Hand Attack!");
        // Implement hand attack logic
    }

    void ProcessDrawing(List<Vector3> points)
    {
        // Implement the logic to process the drawing
        Debug.Log("Processing drawing with " + points.Count + " points.");
    }
}
/*
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public LineRenderer lineRenderer; // Assign this in the Inspector
    public Transform playerHead; // Assign the player's head transform in the Inspector
    public Transform playerHand; // Assign one of the player's hand transforms in the Inspector
    public float threshold = 2f; // Distance threshold to consider near head or hand
    public float lineThickness = 0.1f;
    public Camera mainCamera; // Assign the main camera in the Inspector

    private List<Vector3> points;

    void Start()
    {
        points = new List<Vector3>();
        lineRenderer.startWidth = lineThickness;
        lineRenderer.endWidth = lineThickness;
        lineRenderer.positionCount = 0;

        // Ensure the LineRenderer has a material
        if (lineRenderer.material == null)
        {
            Debug.LogWarning("LineRenderer does not have a material assigned.");
        }
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = touch.position;
            touchPosition.z = mainCamera.nearClipPlane; // Adjust based on your camera's setup
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);
            worldPosition.z = 0; // Ensure the drawing is done on the same plane

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    points.Clear();
                    lineRenderer.positionCount = 0;
                    points.Add(worldPosition);
                    lineRenderer.positionCount = 1;
                    lineRenderer.SetPosition(0, worldPosition);
                    DetermineAttackType(worldPosition); // Check the start point immediately
                    Debug.Log($"Start Point: {worldPosition}");
                    break;

                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    if (points.Count == 0 || Vector3.Distance(points[points.Count - 1], worldPosition) > 0.1f)
                    {
                        points.Add(worldPosition);
                        lineRenderer.positionCount = points.Count;
                        lineRenderer.SetPosition(points.Count - 1, worldPosition);
                        Debug.Log($"Drawing Line to: {worldPosition}");
                    }
                    break;

                case TouchPhase.Ended:
                    lineRenderer.positionCount = 0; // Optionally, clear the line after drawing ends
                    break;
            }
        }
    }

    private void DetermineAttackType(Vector3 startPoint)
    {
        if (IsNearPosition(startPoint, playerHead.position))
        {
            HeadshotAttack();
        }
        else if (IsNearPosition(startPoint, playerHand.position))
        {
            HandAttack();
        }
    }

    private bool IsNearPosition(Vector3 point, Vector3 targetPosition)
    {
        return Vector3.Distance(point, targetPosition) < threshold;
    }

    private void HeadshotAttack()
    {
        Debug.Log("Headshot!");
        // Implement headshot attack logic
    }

    private void HandAttack()
    {
        Debug.Log("Hand Attack!");
        // Implement hand attack logic
    }
}*/





