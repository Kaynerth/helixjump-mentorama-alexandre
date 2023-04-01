using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] Transform target;
    Vector3 offset;
    float smoothSpeed = 0.02f;

    void Start()
    {
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        Vector3 newPos = Vector3.Lerp(transform.position, target.position + offset, smoothSpeed);
        transform.position = newPos;
    }
}
