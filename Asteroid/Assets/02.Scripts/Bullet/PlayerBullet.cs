using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float lifespan = 2.0f;
    public float moveSpeed = 5.0f; // 몬스터 이동 속도
    public int damage = 10; // 총알 데미지
    public float currentTime = 0;
    //사운드 매니저

    void Update()
    {
        // 현재 시간을 증가시킵니다.
        currentTime += Time.deltaTime;

        // 수명이 다한 총알을 처리합니다.
        if (currentTime >= lifespan)
        {
            Destroy(gameObject); // 총알을 파괴합니다.
        }

        // 몬스터를 오른쪽으로 이동
        Vector2 moveDirection = new Vector2(1, 0); // 오른쪽으로 이동하는 방향으로 설정
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 총알이 다른 오브젝트에 충돌했을 때
        if (other.CompareTag("Enemy"))
        {
            // 적 오브젝트에 데미지를 입힘
            MonsterController enemyHealth = other.GetComponent<MonsterController>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
                
            }

            // 총알 제거
            Destroy(gameObject);
        }

        if (other.CompareTag("Money"))
        {
            // 적 오브젝트에 데미지를 입힘
            MonsterController enemyHealth = other.GetComponent<MonsterController>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }

            // 총알 제거
            Destroy(gameObject);
        }
    }
}
