using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    [Range(0.01f, 1.0f)] public float smoothness = 0.5f;
    public float mouseSensitivity = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    void Start()
    {
        offset = transform.position - player.position;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        // Get mouse input
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, -45f, 45f);

        // Calculate rotation based on mouse movement
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        
        // Set camera position relative to the player
        Vector3 desiredPosition = player.position + rotation * offset;
        transform.position = Vector3.Slerp(transform.position, desiredPosition, smoothness);
        
        // Look at the player
        transform.LookAt(player.position);

        // Make the player face the direction the camera is looking
        player.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
    }
}
