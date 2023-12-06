using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotationSpeed = 20f; // 회전 속도 (초당 20도)
    public float moveSpeed = 1f; // 이동 속도
    public float amplitude = 1f; // 위아래로 움직이는 범위

    private Vector3 initialPosition;
    private float time;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        // 물체를 왼쪽으로 이동
        Vector3 moveDirection = Vector3.left;
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        // 물체를 회전
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

        // 물체를 위아래로 움직임
        time += Time.deltaTime;
        float yOffset = Mathf.Sin(time) * amplitude;
        transform.position = new Vector3(transform.position.x, initialPosition.y + yOffset, transform.position.z);
    }
}
