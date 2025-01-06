using UnityEngine;

public class BubbleAreaBoundary : MonoBehaviour
{
    private BoxCollider2D boundary;

    private void Awake()
    {
        boundary = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Bubble"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 velocity = rb.linearVelocity;
                Vector3 bubblePosition = other.transform.position;

                float left = boundary.bounds.min.x;
                float right = boundary.bounds.max.x;
                float top = boundary.bounds.max.y;
                float bottom = boundary.bounds.min.y;

                if (bubblePosition.x <= left || bubblePosition.x >= right)
                {
                    velocity.x = -velocity.x;
                }

                if (bubblePosition.y <= bottom || bubblePosition.y >= top)
                {
                    velocity.y = -velocity.y;
                }

                rb.linearVelocity = velocity;
            }
        }
    }
}