using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    public float moveSpeed = 0.2f;  // �̵� �ӵ�
    public float moveDistance = 0.5f;  // �̵� �Ÿ�

    private bool isMovingUp = true;  // ���� ĳ���Ͱ� ���� �̵� ������ ����

    void Update()
    {
        // ĳ���� �̵� �޼��� ȣ��
        MoveCharacter();
    }

    void MoveCharacter()
    {
        // �̵� ���� ����
        Vector3 moveDirection = isMovingUp ? Vector3.up : Vector3.down;

        // ĳ���͸� �̵� �Ÿ���ŭ �̵�
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        // �̵� �Ÿ���ŭ �̵��ߴ��� üũ
        if (Mathf.Abs(transform.position.y) >= moveDistance)
        {
            // �̵� ���� ����
            isMovingUp = !isMovingUp;
        }
    }
}
