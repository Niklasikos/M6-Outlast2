using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private float bobFrequency = 8f;
    [SerializeField] private float bobVerticalAmount = 0.05f;
    [SerializeField] private float bobHorizontalAmount = 0.04f;
    [SerializeField] private float bobTiltAmount = 2f;
    [SerializeField] private float bobSmoothness = 8f;

    private Vector3 cameraStartPosition;
    private float bobTimer;

    [SerializeField] private float sensitivity = 0.1f;
    [SerializeField] private Transform pCamera;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float acceleration = 15f;
    [SerializeField] private float deceleration = 20f;

    private Vector3 currentVelocity;
    private PlayerInput playerInput;
    private InputAction MouseInput;
    private InputAction moveAction;
    private Rigidbody rb;
    private float xRotation = 0f;

    void OnEnable()
    {
        MouseInput.Enable();
        moveAction.Enable();
    }

    void OnDisable()
    {
        MouseInput.Disable();
        moveAction.Disable();
    }
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Movement"];
        MouseInput = playerInput.actions["Mouse"];
    }
    void Start()
    {
        cameraStartPosition = pCamera.localPosition;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Vector2 mouseDelta = MouseInput.ReadValue<Vector2>();

        float mouseX = mouseDelta.x * sensitivity;
        float mouseY = mouseDelta.y * sensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        HeadBob();

        transform.Rotate(Vector3.up * mouseX);
    }


    private void FixedUpdate()
    {
        Vector2 moveInput = moveAction.ReadValue<Vector2>();

        Vector3 movement = transform.right * moveInput.x + transform.forward * moveInput.y;
        movement = Vector3.ClampMagnitude(movement, 1f);

        Vector3 targetVelocity = movement * speed;

        float accelerationRate = moveInput.magnitude > 0.01f ? acceleration : deceleration;

        currentVelocity = Vector3.MoveTowards(currentVelocity, targetVelocity, accelerationRate * Time.fixedDeltaTime);

        rb.MovePosition(rb.position + currentVelocity * Time.fixedDeltaTime);
    }


    private void HeadBob()
    {
        Vector2 moveInput = moveAction.ReadValue<Vector2>();

        if (moveInput.magnitude > 0.1f)
        {
            bobTimer += Time.deltaTime * bobFrequency;

            float verticalBob = Mathf.Sin(bobTimer * 2f) * bobVerticalAmount;
            float horizontalBob = Mathf.Cos(bobTimer) * bobHorizontalAmount;
            float tilt = Mathf.Sin(bobTimer) * bobTiltAmount;

            Vector3 targetPosition = cameraStartPosition + new Vector3(horizontalBob, verticalBob, 0f);

            pCamera.localPosition = Vector3.Lerp(
                pCamera.localPosition,
                targetPosition,
                bobSmoothness * Time.deltaTime
            );

            Quaternion targetRotation = Quaternion.Euler(
                xRotation,
                0f,
                -tilt
            );

            pCamera.localRotation = Quaternion.Slerp(
                pCamera.localRotation,
                targetRotation,
                bobSmoothness * Time.deltaTime
            );
        }
        else
        {
            bobTimer = 0f;

            pCamera.localPosition = Vector3.Lerp(
                pCamera.localPosition,
                cameraStartPosition,
                bobSmoothness * Time.deltaTime
            );

            pCamera.localRotation = Quaternion.Slerp(
                pCamera.localRotation,
                Quaternion.Euler(xRotation, 0f, 0f),
                bobSmoothness * Time.deltaTime
            );
        }
    }

}
