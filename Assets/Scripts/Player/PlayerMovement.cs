using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Camera cam;          // assign in Inspector
    public Transform body;      // the thing that visually rotates (often same as transform)

    [Header("Movement")]
    public float moveSpeed = 6f;

    [Header("Mouse Look")]
    public float sensitivity = 200f;
    public float pitchClamp = 80f;

    float yaw;   // left/right (around Y)
    float pitch; // up/down (around X)

    void Awake()
    {
        if (!controller) controller = GetComponent<CharacterController>();
    }

    void Start()
    {
        // initialize yaw/pitch to current transforms
        yaw   = body ? body.eulerAngles.y : transform.eulerAngles.y;
        pitch = cam ? cam.transform.localEulerAngles.x : 0f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible   = false;
    }

    void Update()
    {
        // ---------- Mouse look ----------
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        yaw   += mouseX;
        pitch -= mouseY; // invert so moving mouse up looks up
        pitch  = Mathf.Clamp(pitch, -pitchClamp, pitchClamp);

        if (body)
            body.rotation = Quaternion.Euler(0f, yaw, 0f);
        else
            transform.rotation = Quaternion.Euler(0f, yaw, 0f);

        if (cam)
            cam.transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);

        // ---------- Movement (self-relative) ----------
        float forward = Input.GetAxisRaw("Vertical");   // W/S
        float strafe  = Input.GetAxisRaw("Horizontal"); // A/D

        Vector3 local = new Vector3(strafe, 0f, forward);
        local = Vector3.ClampMagnitude(local, 1f);

        // Use the same rotating transform used for yaw
        Transform basis = body ? body : transform;
        Vector3 worldDir = basis.rotation * local;

        Vector3 velocity = worldDir * moveSpeed;

        // CharacterController expects distance per frame, so multiply once by deltaTime
        controller.Move(velocity * Time.deltaTime);

        // gravity (optional)
        if (!controller.isGrounded)
            controller.Move(Physics.gravity * Time.deltaTime);
    }
}