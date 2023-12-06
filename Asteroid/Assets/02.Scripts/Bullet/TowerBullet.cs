using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBullet : MonoBehaviour
{
    private Transform target;
    public float speed = 10f;
    public int damage = 10;
    public SoundManager soundManager;
    public void Seek(Transform _target)
    {
        target = _target;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        // 여기에서 적(Enemy)을 공격하거나 파괴하는 코드를 추가합니다.
        // 예: target.GetComponent<Enemy>().TakeDamage(damage);
        Destroy(gameObject);

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
                soundManager.PlaySound(0);
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
