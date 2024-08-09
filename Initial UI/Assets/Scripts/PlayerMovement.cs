using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float forwardMovementSpeed = 5f;
    public float backwardMovementSpeed = 3f;
    public float jumpForce = 10f;
    public float forwardJumpForce = 15f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void MoveForward()
    {
        rb.velocity = new Vector3(forwardMovementSpeed, rb.velocity.y, 0);
    }

    public void MoveBackward()
    {
        rb.velocity = new Vector3(-backwardMovementSpeed, rb.velocity.y, 0);
    }

    public void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}