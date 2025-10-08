using Codice.Client.BaseCommands.CheckIn.Progress;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Camera cam;
    public Transform body;
    public InputActionAsset inputActions;

    public InputAction moveAction;
    public InputAction lookAction;

    public InputAction sneakAction;

    [Header("Movement")]
    public float moveSpeed = 6f;

    [Header("Mouse Look")]
    public float sensitivity = 5f;
    public float pitchClamp = 80f;

    float yaw;
    float pitch;

    void Awake()
    {
        moveAction = inputActions.FindAction("Move");
        lookAction = inputActions.FindAction("Look");
        sneakAction = inputActions.FindAction("Sneak");
        moveAction.Enable();
        sneakAction.Enable();
    }

    void Start()
    {
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            lookAction.Enable();
        }
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            lookAction.Disable();
        }
    }

    void FixedUpdate()
    {
        Move();
        Look();
    }

    void Move()
    {
        var speed = moveSpeed;
        if (sneakAction.IsPressed())
        {
            speed = moveSpeed / 2f;
        }
        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 move = transform.right * input.x + transform.forward * input.y;
        controller.Move(move * speed * Time.fixedDeltaTime);
    }


    void Look()
    {
        Vector2 mouseDelta = lookAction.ReadValue<Vector2>();
        yaw += mouseDelta.x * sensitivity * Time.fixedDeltaTime;
        pitch -= mouseDelta.y * sensitivity * Time.fixedDeltaTime;

        pitch = Mathf.Clamp(pitch, -pitchClamp, pitchClamp);

        body.localRotation = Quaternion.Euler(0f, yaw, 0f);
        cam.transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }


}