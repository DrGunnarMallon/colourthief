using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private LayerMask bubbleLayerMask;

    private bool hasColor = false;
    private ColorData currentColor;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    private ColorManager colorManager;

    [Header("Transfer Cooldown")]
    [SerializeField] private float transferCooldown = 3f;
    private float lastTransferTime = -999f;


    #region Class Setup Methods

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        colorManager = FindFirstObjectByType<ColorManager>();
    }

    private void OnEnable()
    {
        EventsManager.Instance.OnLevelCompleted += ResetPlayer;
        EventsManager.Instance.OnResetGame += ResetPlayer;
    }

    private void OnDisable()
    {
        EventsManager.Instance.OnLevelCompleted -= ResetPlayer;
        EventsManager.Instance.OnResetGame -= ResetPlayer;
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            Collider2D hitCollider = Physics2D.OverlapPoint(mousePos, bubbleLayerMask);

            if (hitCollider == null) return;

            BubbleController bubble = hitCollider.GetComponent<BubbleController>();

            if (bubble == null) return;

            bool success = bubble.TryTractor();
        }
    }

    #endregion

    #region Public Methods

    public bool HasColor() => hasColor;
    public Vector3 GetVelocity() => rb.linearVelocity;
    public ColorData GetColor() => currentColor;

    public void ResetPlayer()
    {
        hasColor = false;
        currentColor = null;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        transform.rotation = Quaternion.identity;
        transform.position = spawnPoint.position;
    }

    public void SetColor(ColorData color)
    {
        if (color == null || hasColor) return;

        hasColor = true;
        currentColor = color;
        spriteRenderer.color = color.colorRGB;
    }

    public void ClearColor()
    {
        hasColor = false;
        currentColor = null;
        spriteRenderer.color = Color.gray;
    }

    public ColorData DrainColor()
    {
        ColorData tempColor = currentColor;
        ClearColor();
        return tempColor;
    }


    #endregion

    #region Helper Methods

    private bool CanTransfer()
    {
        return (Time.time - lastTransferTime >= transferCooldown);
    }

    private void RecordTransfer()
    {
        lastTransferTime = Time.time;
    }

    #endregion
}
