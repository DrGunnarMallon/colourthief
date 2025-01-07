using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class BubbleController : MonoBehaviour
{
    [Header("Bubble Movement")]
    [SerializeField] private float movementStrength = 0.5f;
    [SerializeField] private float maxSpeed = 2f;
    [SerializeField] private float tractorSpeed = 3f;

    [Header("Attraction Settings")]
    [SerializeField] private float maxAttractionRange = 3f;

    [Header("Effects")]
    [SerializeField] private ParticleSystem destroyParticleSystem;

    [Header("Beacon Settings")]
    [SerializeField] private GameObject attractionBeaconPrefab;

    private ColorData currentColor;
    private bool hasColor = true;
    private bool isTractored = false;
    private bool isAttachedToPlayer = false;
    private Transform tractorTarget;

    private SpriteRenderer spriteRenderer;
    private TextMeshPro label;
    private Rigidbody2D rb;
    private Collider2D bubbleCollider;
    private PlayerInput playerInput;

    private PlayerController playerController;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        label = GetComponentInChildren<TextMeshPro>();
        destroyParticleSystem = GetComponentInChildren<ParticleSystem>();
        rb = GetComponent<Rigidbody2D>();
        bubbleCollider = GetComponent<Collider2D>();
        playerController = FindFirstObjectByType<PlayerController>();
    }

    private void Start()
    {
        ApplyRandomImpulse();
        InvokeRepeating(nameof(ApplyGentleForce), 0f, 2f);
    }

    private void Update()
    {
        if (isTractored && tractorTarget != null && !isAttachedToPlayer)
        {
            float step = tractorSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, tractorTarget.position, step);

            float distanceToPlayer = Vector2.Distance(transform.position, tractorTarget.position);
            if (distanceToPlayer < 0.1f)
            {
                AttachToPlayer();
            }
        }
    }

    private void FixedUpdate()
    {
        if (!isTractored && rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
    }

    private void ApplyRandomImpulse()
    {
        Vector2 impulse = Random.insideUnitCircle.normalized * movementStrength;
        rb.AddForce(impulse, ForceMode2D.Impulse);
    }

    private void ApplyGentleForce()
    {
        if (!isTractored)
        {
            Vector2 randomForce = Random.insideUnitCircle * movementStrength;
            rb.AddForce(randomForce, ForceMode2D.Force);
        }
    }

    public bool TryTractor()
    {
        if (!hasColor || isTractored || isAttachedToPlayer) return false;

        float distanceToPlayer = Vector2.Distance(
            playerController.transform.position,
            transform.position
        );

        if (distanceToPlayer > maxAttractionRange)
        {
            ShowAttractionLimitBeacon(maxAttractionRange);
            return false;
        }

        AudioManager.Instance.PlaySound(AudioManager.AudioType.Tractor);

        isTractored = true;

        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.bodyType = RigidbodyType2D.Kinematic;

        Collider2D playerCollider = playerController.GetComponent<Collider2D>();
        if (playerCollider)
        {
            Physics2D.IgnoreCollision(bubbleCollider, playerCollider, true);
        }

        tractorTarget = playerController.transform;
        TractorBeam.Instance.SetSourceObject(playerController.transform);
        TractorBeam.Instance.SetTargetObject(transform);
        TractorBeam.Instance.Activate();

        // playerController.SetTrackedBubble(this);

        return true;
    }

    private void ShowAttractionLimitBeacon(float maxRange)
    {
        if (attractionBeaconPrefab != null && playerController != null)
        {
            GameObject beacon = Instantiate(attractionBeaconPrefab, playerController.transform.position, Quaternion.identity);

            BeaconController beaconController = beacon.GetComponent<BeaconController>();

            if (beaconController != null)
            {
                beaconController.ActivateBeacon(playerController.transform.position, maxRange);
            }
        }
    }


    private void AttachToPlayer()
    {
        isAttachedToPlayer = true;
        transform.SetParent(tractorTarget);
        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.one;
        label.enabled = false;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Container") && isTractored && hasColor)
        {
            ContainerController container = other.GetComponent<ContainerController>();
            if (container != null)
            {
                // container.ReceiveColor(currentColor);
                StartCoroutine(PlayDestroyAnimation());
            }
        }
    }

    private IEnumerator PlayDestroyAnimation()
    {
        if (destroyParticleSystem != null)
        {
            destroyParticleSystem.transform.SetParent(null);
            destroyParticleSystem.transform.position = transform.position;
            ParticleSystem.MainModule mainModule = destroyParticleSystem.main;
            mainModule.startColor = currentColor.colorRGB;
            destroyParticleSystem.Play();
            Destroy(destroyParticleSystem.gameObject, destroyParticleSystem.main.duration);
        }

        AudioManager.Instance.PlaySound(AudioManager.AudioType.DrainPaint);

        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }

    #region Public Methods

    public bool HasColor() => hasColor;

    public void SetBubbleColor(ColorData newColor)
    {
        currentColor = newColor;
        spriteRenderer.color = currentColor.colorRGB;
        label.text = currentColor.colorName;
        hasColor = true;
    }

    public void Release()
    {
        if (!isTractored && !isAttachedToPlayer) return;
        transform.SetParent(null);

        rb.bodyType = RigidbodyType2D.Dynamic;

        Start();

        transform.rotation = Quaternion.identity;

        Collider2D playerCollider = playerController.GetComponent<Collider2D>();
        if (playerCollider)
        {
            Physics2D.IgnoreCollision(bubbleCollider, playerCollider, false);
        }

        isTractored = false;
        isAttachedToPlayer = false;
        label.enabled = true;
    }

    #endregion
}
