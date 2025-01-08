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
    private bool isFrozen = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        EventsManager.Instance.OnFreezePlayer += FreezePlayer;
        EventsManager.Instance.OnUnfreezePlayer += UnfreezePlayer;
        EventsManager.Instance.OnNextLevel += UnfreezePlayer;
    }

    private void OnDisable()
    {
        EventsManager.Instance.OnFreezePlayer -= FreezePlayer;
        EventsManager.Instance.OnUnfreezePlayer -= UnfreezePlayer;
        EventsManager.Instance.OnNextLevel -= UnfreezePlayer;
    }

    #region Input System Events

    private void FreezePlayer()
    {
        isFrozen = true;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        moveInput = Vector2.zero;
    }

    private void UnfreezePlayer() => isFrozen = false;

    public void OnMove(InputValue value)
    {
        if (isFrozen) return;

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