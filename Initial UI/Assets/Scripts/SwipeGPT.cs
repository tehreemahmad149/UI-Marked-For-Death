using UnityEngine;

public class SwipeDetector : MonoBehaviour
{
    public GameObject trailPrefab; // Assign in Inspector
    private GameObject currentTrail;
    private Vector2 touchStartPos;
    private Vector2 touchEndPos;
    private float swipeThreshold = 50f; // Minimum distance for a swipe
    private GameObject player;
    private Fighter fighter;

    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                fighter = player.GetComponent<Fighter>();
            }
            else
            {
                Debug.Log("Looking for player...");
                return; // Exit Update if player is not detected
            }
        }
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

        float angle = Mathf.Atan2(swipeDelta.y, swipeDelta.x) * Mathf.Rad2Deg;
        if (fighter.isAlive && !fighter.isAttacking)
        {
            if (angle >= 67.5f && angle < 112.5f)
            {
                fighter.jump();
            }
            else if (angle >= 112.5f && angle < 157.5f)
            {
                if (!fighter.inFront)
                {
                    fighter.StrongSlash();
                }
            }
            else if (angle >= 22.5f && angle < 67.5f)
            {
                if (fighter.inFront)
                {
                    fighter.StrongSlash();
                }
            }
            else if (angle >= -22.5f && angle < 22.5f)
            {
                if (fighter.inFront)
                {
                    fighter.MoveForth();
                }
                else {
                    fighter.MoveBack();
                }
            }
            else if (angle >= -67.5f && angle < -22.5f)
            {
                if (!fighter.inFront)
                {
                    fighter.WideSlash();
                }
                else
                {
                    fighter.QuickSlash();
                }
            }
            else if (angle >= -112.5f && angle < -67.5f)
            {
                Debug.Log("Parry");
                fighter.parry();
            }
            else if (angle >= -157.5f && angle < -112.5f)
            {
                if (fighter.inFront)
                {
                    fighter.WideSlash();
                }
                else
                {
                    fighter.QuickSlash();
                }
            }
            else if ((angle >= 157.5f && angle <= 180f) || (angle >= -180f && angle < -157.5f))
            {
                if (!fighter.inFront)
                {
                    fighter.MoveForth();
                }
                else
                {
                    fighter.MoveBack();
                }
            }
        }
    }
}
