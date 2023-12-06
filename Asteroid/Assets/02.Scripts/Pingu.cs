using UnityEngine;

public class Pingu : MonoBehaviour
{
    public float moveSpeed = 5f;  // 이동 속도 조절을 위한 변수
    public float maxY = 5f;       // 이동 가능한 최대 Y 좌표
    public float minY = -5f;      // 이동 가능한 최소 Y 좌표

    private int direction = 1;    // 이동 방향을 나타내는 변수 (1: 위, -1: 아래)

    void Update()
    {
        // 물체를 현재 이동 방향과 속도에 따라 이동시킵니다.
        transform.Translate(Vector3.up * direction * moveSpeed * Time.deltaTime);

        // 물체가 일정 범위를 벗어나면 방향을 반전시킵니다.
        if (transform.position.y >= maxY || transform.position.y <= minY)
        {
            ChangeDirection();
        }
    }

    // 이동 방향을 반전시키는 함수
    void ChangeDirection()
    {
        direction *= -1;
    }
}
