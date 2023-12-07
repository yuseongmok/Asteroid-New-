using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Henchman : MonoBehaviour
{
    public int maxHp = 500;        // 최대 체력
    public int currentHp;           // 현재 체력

    // 패턴 계산
    public float timer;
    public float cooltime;
    public Attack Hench = Attack.Off;

    // Attack
    public GameObject HenchAttack;
    public GameObject warningEffect;
    public Transform AttackSpawnPoint;

    public enum Attack
    {
        On,
        Off,
        warning,
    }

    void Start()
    {
        currentHp = maxHp;
        timer = 0f;
    }


    void Update()
    {
        OnAttack();
    }

    void OnAttack()
    {
        if (Hench == Attack.Off)
        {
            timer += Time.deltaTime;
            cooltime = 2f;
            if (timer > cooltime)
            {
                Hench = Attack.warning;
                timer = 0f;
            }
        }
        else if (Hench == Attack.warning)
        {
            timer += Time.deltaTime;
            cooltime = 2f;
            if (timer > cooltime)
            {
                ShowWarning();
                Hench = Attack.On;
                timer = 0f;
            }
        }
        else if (Hench == Attack.On)
        {
            timer += Time.deltaTime;
            cooltime = 1.1f;
            if (timer > cooltime)
            {
                Attack1();
                Hench = Attack.Off;
                timer = 0f;
            }
        }
    }

    void Attack1()
    {
        Instantiate(HenchAttack, AttackSpawnPoint.position, AttackSpawnPoint.rotation);
    }

    private void ShowWarning()
    {
        StartCoroutine(CoShowWarning());

    }
    IEnumerator CoShowWarning()
    {
        GameObject temp = Instantiate(warningEffect, AttackSpawnPoint.position, AttackSpawnPoint.rotation);
        Destroy(temp, 0.3f);
        yield return new WaitForSeconds(0.4f);
        GameObject temp2 = Instantiate(warningEffect, AttackSpawnPoint.position, AttackSpawnPoint.rotation);
        Destroy(temp2, 0.3f);
        yield return new WaitForSeconds(0.4f);
    }

    public void TakeDamage(int damage)   // 데미지 받는 판정
    {
        currentHp -= damage;            // 데미지만큼 현재 체력 감소
        if (currentHp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
