using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    private bool hasColor = false;
    private ColorData currentColor;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    private ColorData emptyColor;

    private BubbleController trackedBubble = null;

    [SerializeField] private LayerMask bubbleLayerMask;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            Collider2D hitCollider = Physics2D.OverlapPoint(mousePos, bubbleLayerMask);

            if (hitCollider == null)
            {
                ReleaseBubble();
            }
            else
            {

                BubbleController bubble = hitCollider.GetComponent<BubbleController>();
                if (bubble == null)
                {
                    ReleaseBubble();
                }
                else
                {
                    bool success = bubble.TryTractor();
                    if (!success)
                    {
                        ReleaseBubble();
                    }
                }
            }
        }
    }

    #region Public Methods

    public void BorrowColor(ColorData color)
    {
        if (color == null) return;
        hasColor = true;
        currentColor = color;
        spriteRenderer.color = color.colorRGB;
    }

    public ColorData DepositColor()
    {
        hasColor = false;
        ColorData tempColor = currentColor;
        currentColor = ColorManager.Instance.GetColorByName("White");
        spriteRenderer.color = currentColor.colorRGB;
        return tempColor;
    }

    public bool HasColor() => hasColor;

    public void Stop()
    {
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }

    public void SetTrackedBubble(BubbleController bubble)
    {
        if (trackedBubble != null && trackedBubble != bubble)
        {
            trackedBubble.Release();
        }

        trackedBubble = bubble;
    }

    public void ReleaseBubble()
    {
        if (trackedBubble != null)
        {
            trackedBubble.Release();
            trackedBubble = null;
            TractorBeam.Instance.Deactivate();
            AudioManager.Instance.PlayReleaseClip();
        }
    }

    public Vector3 GetVelocity() => rb.linearVelocity;

    public void ResetPlayer(Transform spawnPoint)
    {
        hasColor = false;
        currentColor = null;
        trackedBubble = null;
        transform.rotation = Quaternion.identity;
        transform.position = spawnPoint.position;
    }

    #endregion
}
