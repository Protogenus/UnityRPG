using UnityEngine;
using UnityEngine.InputSystem;

public class CameraOrbit : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0, 5, -8);
    public float rotateSpeed = 2f;  // tweak in Inspector
    public float minPitch = -20f;   // lowest angle
    public float maxPitch = 60f;    // highest angle

    private float yaw = 0f;
    private float pitch = 20f;      // start slightly above player
    private Vector2 lookInput;

    private PlayerControls controls;

    private void Awake()
    {
        controls = new PlayerControls();

        // Mouse input
        controls.Player.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        controls.Player.Look.canceled += ctx => lookInput = Vector2.zero;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    void LateUpdate()
    {
        // Rotate camera based on mouse input
        yaw += lookInput.x * rotateSpeed * 100f * Time.deltaTime;
        pitch -= lookInput.y * rotateSpeed * 100f * Time.deltaTime; // subtract to invert Y

        // Clamp vertical angle
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        // Calculate new camera position
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        transform.position = player.position + rotation * offset;

        // Look at player
        transform.LookAt(player.position + Vector3.up * 1.5f);
    }
}
