using UnityEngine;

public class KeepLabelUpright : MonoBehaviour
{
    private void LateUpdate()
    {

        transform.localEulerAngles = Vector3.zero;
    }
}
