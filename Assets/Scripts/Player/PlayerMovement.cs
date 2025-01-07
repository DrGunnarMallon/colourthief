using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float rotationSpeed = 6f;

    private Vector2 moveInput;
    private Rigidbody2D rb;
    private PlayerInput playerInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
    }

    #region Input System Events

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        float vertical = moveInput.y;
        float horizontal = moveInput.x;

        float rotationAmount = -horizontal * rotationSpeed * Time.fixedDeltaTime;
        float newRotation = rb.rotation + rotationAmount;

        rb.MoveRotation(newRotation);

        Vector2 forwardDir = new Vector2(
            Mathf.Cos((newRotation + 90) * Mathf.Deg2Rad),
            Mathf.Sin((newRotation + 90) * Mathf.Deg2Rad)
        );

        rb.linearVelocity = forwardDir * vertical * moveSpeed;
    }

    public Vector3 GetVelocity() => rb.linearVelocity;

    #endregion
}