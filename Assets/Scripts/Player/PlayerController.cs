using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;

    [Header("Transfer Cooldown")]
    [SerializeField] private float transferCooldown = 3f;
    private float lastTransferTime = -999f;

    private bool hasColor = false;
    private ColorData currentColor;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    #region Class Setup Methods

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        EventsManager.Instance.OnLevelCompleted += ResetPlayer;
        EventsManager.Instance.OnNewLevel += ResetPlayer;
        EventsManager.Instance.OnColorCaptured += SetColor;
        EventsManager.Instance.OnNextLevel += ResetPlayer;
    }

    private void OnDisable()
    {
        EventsManager.Instance.OnLevelCompleted -= ResetPlayer;
        EventsManager.Instance.OnNewLevel -= ResetPlayer;
        EventsManager.Instance.OnColorCaptured -= SetColor;
        EventsManager.Instance.OnNextLevel -= ResetPlayer;
    }

    #endregion

    #region Public Methods

    public bool HasColor() => hasColor;
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

    public ColorData DrainColor()
    {
        ColorData tempColor = currentColor;
        ClearColor();
        return tempColor;
    }

    #endregion

    #region Helper Methods

    private void ClearColor()
    {
        hasColor = false;
        currentColor = null;
        spriteRenderer.color = Color.gray;
    }

    public bool CanTransfer() => (Time.time - lastTransferTime >= transferCooldown);
    public void RecordTransfer() => lastTransferTime = Time.time;

    #endregion
}
