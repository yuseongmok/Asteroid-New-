using UnityEngine;

public class Camara : MonoBehaviour
{
    public Transform player; // 카메라가 따라다닐 대상 플레이어

    void Update()
    {
        if (player != null)
        {
            // 플레이어의 현재 위치를 받아와 카메라의 위치를 업데이트
            Vector3 targetPosition = new Vector3(player.position.x, player.position.y, transform.position.z);
            transform.position = targetPosition;
        }
    }
}
