using UnityEngine;

public class NewMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 targetPosition;
    private float moveTime;
    private float moveStartTime;
    private bool isMoving = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            float t = (Time.time - moveStartTime) / moveTime;
            Vector3 newPosition = Vector3.Lerp(rb.position, targetPosition, t);

            rb.MovePosition(newPosition);

            if (t >= 1.0f)
            {
                isMoving = false;
                rb.velocity = Vector3.zero;
            }
        }
    }

    public void MoveTo(float distance, float duration)
    {
        if (!isMoving)
        {
            targetPosition = rb.position + new Vector3(distance, 0, 0);
            moveTime = duration;
            moveStartTime = Time.time;
            isMoving = true;
        }
    }

    public void Jump(float jumpForce)
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    public void MoveForward(float distance, float duration)
    {
        MoveTo(distance, duration);
    }
    public void ForwardDash(float forwardMovementSpeed)
    {
        rb.velocity = new Vector3(forwardMovementSpeed, rb.velocity.y, 0);
    }

    public void MoveBackward(float distance)
    {

        rb.velocity = new Vector3(distance, rb.velocity.y, 0);
    }
    public void MoveForward2(float distance)
    {
            rb.velocity = new Vector3(distance, rb.velocity.y, 0);
    }
}
