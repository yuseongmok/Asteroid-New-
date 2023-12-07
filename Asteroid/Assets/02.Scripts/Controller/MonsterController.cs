using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public float moveSpeed = 5.0f; // 몬스터 이동 속도
    public int maxHealth = 50; // 최대 체력
    private int currentHealth; // 현재 체력
    public int damage;
    public PlayerHealth player;
    public SoundManager soundManager;

    // Slow Bullet 구현
    private float slow;
    private float timer = 0;
    private float currentTime = 1f;
    private bool SlowTF = false;

    void Start()
    {
        currentHealth = maxHealth; // 시작할 때 현재 체력을 최대 체력으로 설정
        player = GameObject.Find("PlayerBody").GetComponent<PlayerHealth>();
        slow = moveSpeed;
    }
    void Update()
    {
        // 몬스터를 오른쪽으로 이동
        Vector2 moveDirection = new Vector2(-1, 0); // 오른쪽으로 이동하는 방향으로 설정
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        if(SlowTF == true)  // Slow 지속 시간
        {
            timer += Time.deltaTime;

            if (timer >= currentTime)
            {
                SlowTime();
                SlowTF = false;
                timer = 0;
            }
        }
    }

    // 적 캐릭터에게 데미지를 입히는 함수
    public void TakeDamage(int damage)
    {
        soundManager.PlaySound(0);
        currentHealth -= damage; // 데미지만큼 현재 체력 감소
        Debug.Log("적이 공격을 맞음");

        if (currentHealth <= 0)
        {
            Die(); // 체력이 0 이하로 떨어지면 사망 처리
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.TakeDamage1(damage);
            //TakeDamage(player.TakeDamage(damage));
            Destroy(gameObject);
        }

        if(other.CompareTag("Bullet_Slow"))
        {
            moveSpeed = slow * 0.7f;    // 이동속도 50% 느려짐
            SlowTF = true;
        }
    }

    private void SlowTime()     // 기존 이동속도로 되돌아옴
    {
        moveSpeed = slow;
    }

    void Die()
    {
        
        if (gameObject.CompareTag("Money"))
        {
            MoneyManager.Instance.AddMoney(25);
            Debug.Log("돈 운석 파괴됨");
        }
            // 여기에 적 캐릭터가 사망할 때 수행할 작업을 추가할 수 있습니다.
            // 예: 점수 증가, 이펙트 재생 등
            gameObject.SetActive(false);

        if (gameObject.CompareTag("Money2"))
        {
            MoneyManager.Instance.AddMoney(50);
            Debug.Log("돈 운석 파괴됨");
        }
        // 여기에 적 캐릭터가 사망할 때 수행할 작업을 추가할 수 있습니다.
        // 예: 점수 증가, 이펙트 재생 등
        gameObject.SetActive(false);
    }

    internal void ResetHealth()
    {
        throw new NotImplementedException();
    }
}
