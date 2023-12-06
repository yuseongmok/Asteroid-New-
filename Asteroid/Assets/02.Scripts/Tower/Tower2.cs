
using UnityEngine;

public class Tower2 : MonoBehaviour
{
    public Transform target; // Enemy 타겟
    public Transform gunTransform; // 총알 발사 위치
    public GameObject bulletPrefab; // 총알 프리팹
    public float range = 10f; // 사정 거리
    public float fireRate = 1f; // 발사 속도
    private float fireCountdown = 0f;
    public SoundManager soundManager;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.1f); // 주기적으로 타겟 업데이트
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    void Update()
    {
        if (target == null)
        {
            return;
        }

        if (fireCountdown <= 0)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        // 타겟 방향 벡터를 얻어옵니다.
        Vector3 targetDirection = (target.position - gunTransform.position).normalized;

        // Z축 회전 각도를 계산합니다.
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;

        // 총알 발사 위치의 Z축 회전을 설정합니다.
        gunTransform.rotation = Quaternion.Euler(0f, 0f, angle);

        // 두 개의 총알 행 사이의 수직 오프셋을 계산합니다.
        float verticalOffset = 0.25f; // 수직 오프셋을 조절하여 두 총알 행 사이의 간격을 제어합니다.

        // 두 개의 총알 행에 대한 위치를 계산합니다.
        Vector3 position1 = gunTransform.position + gunTransform.up * verticalOffset;
        Vector3 position2 = gunTransform.position - gunTransform.up * verticalOffset;

        // 두 총알 행에 대한 Z-축 회전을 설정합니다.
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

        GameObject bulletGO1 = Instantiate(bulletPrefab, position1, rotation);
        GameObject bulletGO2 = Instantiate(bulletPrefab, position2, rotation);

        TowerBullet bullet1 = bulletGO1.GetComponent<TowerBullet>();
        TowerBullet bullet2 = bulletGO2.GetComponent<TowerBullet>();
        soundManager.PlaySound(0);
        if (bullet1 != null || bullet2 != null)
        {
            bullet1.Seek(target);
            bullet2.Seek(target);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    // 여기에 적의 동작 및 특성을 구현합니다.

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Tower turret = other.GetComponent<Tower>();
            if (turret != null)
            {
                turret.target = transform;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Tower turret = other.GetComponent<Tower>();
            if (turret != null && turret.target == transform)
            {
                turret.target = null;
            }
        }

    }
}