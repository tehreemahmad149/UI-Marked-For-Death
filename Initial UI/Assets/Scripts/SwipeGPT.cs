using UnityEngine;

public class SwipeDetector : MonoBehaviour
{
    public GameObject trailPrefab; // Assign in Inspector
    private GameObject currentTrail;
    private Vector2 touchStartPos;
    private Vector2 touchEndPos;
    private float swipeThreshold = 50f; // Minimum distance for a swipe

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchStartPos = touch.position;
                    CreateTrail(touch.position);
                    break;

                case TouchPhase.Moved:
                    UpdateTrail(touch.position);
                    break;

                case TouchPhase.Ended:
                    touchEndPos = touch.position;
                    DestroyTrail();
                    DetectSwipe();
                    break;
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            touchStartPos = Input.mousePosition;
            CreateTrail(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0))
        {
            UpdateTrail(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            touchEndPos = Input.mousePosition;
            DestroyTrail();
            DetectSwipe();
        }
    }

    void CreateTrail(Vector2 position)
    {
        if (trailPrefab != null)
        {
            currentTrail = Instantiate(trailPrefab, Camera.main.ScreenToWorldPoint(new Vector3(position.x, position.y, 10f)), Quaternion.identity);
            currentTrail.transform.parent = transform;
        }
    }

    void UpdateTrail(Vector2 position)
    {
        if (currentTrail != null)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(position.x, position.y, 10f));
            currentTrail.transform.position = worldPosition;
        }
    }

    void DestroyTrail()
    {
        if (currentTrail != null)
        {
            Destroy(currentTrail);
        }
    }

    void DetectSwipe()
    {
        Vector2 swipeDelta = touchEndPos - touchStartPos;

        if (swipeDelta.magnitude < swipeThreshold)
        {
            return; // Not a swipe
        }

        float x = swipeDelta.x;
        float y = swipeDelta.y;

        if (Mathf.Abs(x) > Mathf.Abs(y))
        {
            if (x > 0)
            {
                Debug.Log("Swipe Right");
            }
            else
            {
                Debug.Log("Swipe Left");
            }
        }
        else
        {
            if (y > 0)
            {
                Debug.Log("Swipe Up");
            }
            else
            {
                Debug.Log("Swipe Down");
            }
        }
    }
}
