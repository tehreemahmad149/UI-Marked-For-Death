using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;  // Reference to the player transform
    public float distanceZ = -10f;  // Distance to maintain in the Z coordinate
    public float heightOffset = 0f; // Height offset for the camera
    public float xOffset = 0f; // X offset for the camera

    private void LateUpdate()
    {
        if (player != null)
        {
            // Calculate the new position based on the player's position and the offsets
            Vector3 newPosition = player.position;
            newPosition.x += xOffset; // Apply X offset
            newPosition.z = player.position.z + distanceZ;  // Maintain the Z distance
            newPosition.y += heightOffset;  // Apply height offset

            // Set the camera's position to the new position
            transform.position = newPosition;

            // Calculate the direction to look at the player
            Vector3 lookAtPosition = player.position + Vector3.up * heightOffset;
            Vector3 direction = lookAtPosition - transform.position;

            // Zero out the X component of the direction vector to prevent angle adjustment for X offset
            direction.x = 0;

            // Make the camera look at the adjusted look-at position
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
