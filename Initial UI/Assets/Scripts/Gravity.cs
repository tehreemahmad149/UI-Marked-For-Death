using UnityEngine;

public class Gravity : MonoBehaviour
{
    public Vector3 gravityDirection = Vector3.down;
    public float gravityMagnitude = 9.81f;

    private void FixedUpdate()
    {
        Rigidbody[] rigidbodies = FindObjectsOfType<Rigidbody>();

        foreach (Rigidbody rb in rigidbodies)
        {
            rb.AddForce(gravityDirection * gravityMagnitude, ForceMode.Acceleration);
        }
    }
}