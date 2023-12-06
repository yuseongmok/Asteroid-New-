using UnityEngine;

public class Main_Void : MonoBehaviour
{
    public float tiltAngle = 30.0f; // 기울기 각도 설정
    public float tiltSpeed = 2.0f; // 기울기 속도 설정
    private float currentTilt = 0.0f; // 현재 기울기

    void Update()
    {
        // 이미지를 부드럽게 왔다갔다하도록 만드는 부분
        float targetTilt = Mathf.Sin(Time.time * tiltSpeed) * tiltAngle;
        currentTilt = Mathf.LerpAngle(currentTilt, targetTilt, Time.deltaTime);

        transform.rotation = Quaternion.Euler(0, 0, currentTilt);
    }
}