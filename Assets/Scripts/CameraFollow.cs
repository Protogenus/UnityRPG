using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;      // Drag your player here in Inspector
    public Vector3 offset = new Vector3(0, 10, -10);
    public float smoothSpeed = 0.125f;

    void LateUpdate()
    {
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(player);
    }
}
