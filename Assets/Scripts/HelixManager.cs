using UnityEngine;

public class HelixManager : MonoBehaviour
{
    void Update()
    {
        if (Time.timeScale == 1)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Moved)
                {
                    transform.Rotate(Vector3.up * -touch.deltaPosition.x);
                }
            }
        }
    }
}
