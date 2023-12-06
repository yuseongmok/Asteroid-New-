using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower3 : MonoBehaviour
{
    public Transform target; // Enemy Ÿ��
    public Transform gunTransform; // �Ѿ� �߻� ��ġ
    public GameObject bulletPrefab; // �Ѿ� ������
    public float range = 10f; // ���� �Ÿ�
    public float fireRate = 1f; // �߻� �ӵ�
    private float fireCountdown = 0f;
    public SoundManager soundManager;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.1f); // �ֱ������� Ÿ�� ������Ʈ
    }

    // �߰��� �κ�: 0.2�� �������� 5�� �߻��ϴ� �޼���
    void ShootMultipleBullets()
    {
        if (target != null)
        {
            for (int i = 0; i < 8; i++)
            {
                StartCoroutine(ShootWithDelay(i * 0.1f));
            }
        }
    }

    // �߰��� �κ�: ���� �ð��� ������ �߻� �޼���
    IEnumerator ShootWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Shoot();
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
            ShootMultipleBullets();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        if (target == null)
        {
            return;
        }

        // Ÿ�� ���� ���͸� ���ɴϴ�.
        Vector3 targetDirection = (target.position - gunTransform.position).normalized;

        // Z�� ȸ�� ������ ����մϴ�.
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;

        // �Ѿ� �߻� ��ġ�� Z�� ȸ���� �����մϴ�.
        gunTransform.rotation = Quaternion.Euler(0f, 0f, angle);

        GameObject bulletGO = Instantiate(bulletPrefab, gunTransform.position, gunTransform.rotation);
        TowerBullet bullet = bulletGO.GetComponent<TowerBullet>();
        soundManager.PlaySound(0);
        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    // ���⿡ ���� ���� �� Ư���� �����մϴ�.

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