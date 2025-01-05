using UnityEngine;

public class TractorBeam : MonoBehaviour
{
    public Transform sourceObject;
    public Transform targetObject;

    private LineRenderer lineRenderer;
    [SerializeField] private bool isActive = false;

    public static TractorBeam Instance { get; private set; }

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

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false;
    }

    private void Update()
    {
        if (isActive)
        {
            if (sourceObject != null && targetObject != null)
            {
                lineRenderer.SetPosition(0, sourceObject.position);
                lineRenderer.SetPosition(1, targetObject.position);
            }
            else
            {
                isActive = false;
                lineRenderer.enabled = false;
            }
        }
    }

    public void Activate()
    {
        isActive = true;
        lineRenderer.enabled = true;
    }

    public void Deactivate()
    {
        isActive = false;
        lineRenderer.enabled = false;
    }

    public void SetSourceObject(Transform source)
    {
        sourceObject = source;
    }

    public void SetTargetObject(Transform target)
    {
        targetObject = target;
    }

}