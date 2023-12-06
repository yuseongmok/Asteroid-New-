using UnityEngine;

public class RotateImage : MonoBehaviour
{
    public float rotationSpeed = 30.0f; // 회전 속도 (초당 각도)

    void Update()
    {
        // 이미지를 회전시킵니다.
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}
