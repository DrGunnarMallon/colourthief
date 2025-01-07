using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerActions : MonoBehaviour
{
    [SerializeField] private LayerMask bubbleLayerMask;

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
}
