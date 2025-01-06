using System.Collections;
using UnityEngine;

public class BeaconController : MonoBehaviour
{
    [Header("Beacon Settings")]
    [SerializeField] private float expansionDuration = 0.7f;
    [SerializeField] private float waitDuration = 0.2f;

    private SpriteRenderer spriteRenderer;
    private float maxRadius = 3f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ActivateBeacon(Vector3 origin, float maxRange)
    {
        Debug.Log("Beacon activated");
        transform.position = origin;
        spriteRenderer.enabled = true;
        maxRadius = maxRange * 2;
        StartCoroutine(ExpandAndFade());
    }

    private IEnumerator ExpandAndFade()
    {
        float elapsed = 0f;
        Vector3 initialScale = Vector3.zero;
        Vector3 targetScale = Vector3.one * (maxRadius - 0.1f);

        AudioManager.Instance.PlaySound(AudioManager.AudioType.Sonar);

        transform.localScale = initialScale;

        while (elapsed < expansionDuration)
        {
            float t = elapsed / expansionDuration;
            transform.localScale = Vector3.Lerp(initialScale, targetScale, t);

            Color currentColor = spriteRenderer.color;
            currentColor.a = Mathf.Lerp(0.5f, 0f, t);
            spriteRenderer.color = currentColor;

            elapsed += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(waitDuration);

        transform.localScale = targetScale;
        spriteRenderer.enabled = false;
    }


}

