using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Transform pivot;               // 피봇 포인트
    public GameObject bulletPrefab;       // 총알 프리팹
    public Transform firePoint;           // 총알 발사 위치
    public float rotationSpeed = 5.0f;    // 피봇 회전 속도
    public float minAngle = -45.0f;       // 최소 회전 각도
    public float maxAngle = 45.0f;        // 최대 회전 각도

    void Update()
    {
        // 피봇을 마우스 위치로 회전시킴
        RotatePivotToMouse();

        // 마우스 클릭으로 총알 발사
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void RotatePivotToMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        Vector3 direction = (mousePosition - pivot.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // 회전 각도를 제한
        angle = Mathf.Clamp(angle, minAngle, maxAngle);

        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        pivot.rotation = Quaternion.Slerp(pivot.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void Shoot()
    {
        // 총알을 발사 위치에서 생성
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
