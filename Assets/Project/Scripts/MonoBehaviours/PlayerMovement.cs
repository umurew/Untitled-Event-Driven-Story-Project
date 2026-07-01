using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintSpeed = 8f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float gravity = -9.81f;

    private CharacterController characterController;
    private float verticalVelocity = 0f;

    private void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();

        if (cameraTransform == null && Camera.main != null)
            cameraTransform = Camera.main.transform;

        InputManager.Instance.SetCursorState(true);
    }

    private void Update()
    {
        // Handle move input
        Vector2 moveInput = InputManager.Instance.playerActions.Move.ReadValue<Vector2>();

        Vector3 forward = cameraTransform.forward;
        forward.y = 0f;
        forward.Normalize();

        Vector3 right = cameraTransform.right;
        right.y = 0f;
        right.Normalize();

        Vector3 horizontalVelocity = forward * moveInput.y + right * moveInput.x;
        Vector3 finalVelocity;


        // Handle ground check and gravity
        if (characterController.isGrounded)
        {
            /// Reset vertical velocity to prevent infinite falling
            /// Used -2f to keep the controller firmly snapped to slopes and stairs
            if (verticalVelocity < 0f)
                verticalVelocity = -2f;

            // Handle jump input
            if (InputManager.Instance.playerActions.Jump.WasPressedThisFrame())
                verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        else
            verticalVelocity += gravity * Time.deltaTime;

        // Handle sprinting
        if (InputManager.Instance.playerActions.Sprint.IsInProgress())
            finalVelocity = sprintSpeed * horizontalVelocity + Vector3.up * verticalVelocity;
        else
            finalVelocity = moveSpeed * horizontalVelocity + Vector3.up * verticalVelocity;

        characterController.Move(finalVelocity * Time.deltaTime);
    }
}