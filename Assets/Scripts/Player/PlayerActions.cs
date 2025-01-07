using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private LayerMask bubbleLayerMask;
    private PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            Collider2D hitCollider = Physics2D.OverlapPoint(mousePos, bubbleLayerMask);

            if (hitCollider == null)
            {
                if (playerController.HasColor())
                {
                    EventsManager.Instance.TriggerCreateBubble(transform.position, playerController.DrainColor());
                }
                return;
            }

            BubbleController bubble = hitCollider.GetComponent<BubbleController>();

            if (bubble == null) return;

            if (!playerController.HasColor())
            {
                bool success = bubble.TryTractor();
            }
        }
    }
}
