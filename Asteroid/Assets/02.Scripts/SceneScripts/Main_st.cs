using UnityEngine;

public class Main_st : MonoBehaviour
{
    public float speed = 2.0f; // 물체의 움직임 속도
    public float distance = 2.0f; // 움직일 거리 (위아래로)

    private Vector3 startPosition;
    private bool movingUp = true;

    void Start()
    {
        // 시작 위치 저장
        startPosition = transform.position;
    }

    void Update()
    {
        // 물체를 위아래로 움직임
        if (movingUp)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }

        // 일정 거리를 이동하면 방향을 반대로 바꿈
        if (Vector3.Distance(startPosition, transform.position) >= distance)
        {
            movingUp = !movingUp;
        }
    }
}
