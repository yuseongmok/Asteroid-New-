using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    public float moveSpeed = 0.2f;  // 이동 속도
    public float moveDistance = 0.5f;  // 이동 거리

    private bool isMovingUp = true;  // 현재 캐릭터가 위로 이동 중인지 여부

    void Update()
    {
        // 캐릭터 이동 메서드 호출
        MoveCharacter();
    }

    void MoveCharacter()
    {
        // 이동 방향 설정
        Vector3 moveDirection = isMovingUp ? Vector3.up : Vector3.down;

        // 캐릭터를 이동 거리만큼 이동
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        // 이동 거리만큼 이동했는지 체크
        if (Mathf.Abs(transform.position.y) >= moveDistance)
        {
            // 이동 방향 변경
            isMovingUp = !isMovingUp;
        }
    }
}
