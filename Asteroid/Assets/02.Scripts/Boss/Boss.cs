using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    // 보스 체력 관련 코드
    public int maxHp = 5000;        // 최대 체력
    public int currentHp;           // 현재 체력
    public Slider HpBar;            // Hp 바 UI

    // 보스 패턴 계산
    public float timer;
    public float cooltime;
    public Pattern BossPattern = Pattern.normar;

    //  Pattern.attack 
    public GameObject BossAttack;
    public Transform BossAttackSpawn;

    // Pattern.HenchmanSpawn
    public GameObject Henchman;
    public Transform HenchSpawnPoint;

    // Pattern.PinguSpawn
    public GameObject Pingu;
    public Transform PinguSpawnPoint;

    public GameObject warningEffect;
    public Transform warnigSpawnPoint;

    // 데미지 입었을 때 효과
    public GameObject Panal;
    private float damageDisplayTime = 0.2f; // 데미지 표시 시간

    public enum Pattern
    {
        normar,
        attack,
        HenchmanSpawn,
        PinguSpawn,
        BossArm,

    }


    void Start()
    {
        currentHp = maxHp;
        timer = 0f;
        Panal.SetActive(false);
    }


    void Update()
    {
        OnPattern();
    }

    void OnPattern()    // 보스 패턴 관리하는 함수
    {
        if (BossPattern == Pattern.normar)
        {
            timer += Time.deltaTime;
            cooltime = 2f;
            if (timer >= cooltime)
            {
                ShowWarning();
                BossPattern = Pattern.attack;
                timer = 0f;
            }
        }
        else if (BossPattern == Pattern.attack)
        {
            timer += Time.deltaTime;
            cooltime = 2f;
            if (timer >= cooltime)
            {
                Attack();
                BossPattern = Pattern.HenchmanSpawn;
                timer = 0f;
            }
        }
        else if (BossPattern == Pattern.HenchmanSpawn)
        {
            timer += Time.deltaTime;
            cooltime = 3f;
            if (timer >= cooltime)
            {
                HenchmanSpawn1();
                BossPattern = Pattern.PinguSpawn;
                timer = 0f;
            }
        }
        else if (BossPattern == Pattern.PinguSpawn)
        {
            timer += Time.deltaTime;
            cooltime = 2f;
            if (timer >= cooltime)
            {
                PinguSpawn1();
                BossPattern = Pattern.normar;
                timer = 0f;
            }
        }
    }




    public void TakeDamage(int damage)   // 보스가 데미지 받는 판정
    {
        currentHp -= damage;            // 데미지만큼 현재 체력 감소
        HpBar.value = currentHp;        // Slider 줄어듦
        StartCoroutine(DisplayDamagePanal()); // 데미지 패널을 표시하는 코루틴 시작

        if (currentHp <= 500)
        {

        }
        if (currentHp <= 0)
        {
            Destroy(gameObject);
           
        }

    }

    void Attack()   // BossAttack 프리팹 BossAttackSpawn에 생성
    {
        Instantiate(BossAttack, BossAttackSpawn.position, BossAttackSpawn.rotation);
    }

    void HenchmanSpawn1()
    {
        Instantiate(Henchman, HenchSpawnPoint.position, HenchSpawnPoint.rotation);
    }
    void PinguSpawn1()
    {
        Instantiate(Pingu, PinguSpawnPoint.position, PinguSpawnPoint.rotation);
    }

    private void ShowWarning()
    {
        StartCoroutine(CoShowWarning());

    }
    IEnumerator CoShowWarning()
    {
        GameObject temp = Instantiate(warningEffect, warnigSpawnPoint.position, warnigSpawnPoint.rotation);
        Destroy(temp, 0.4f);
        yield return new WaitForSeconds(0.7f);
        GameObject temp2 = Instantiate(warningEffect, warnigSpawnPoint.position, warnigSpawnPoint.rotation);
        Destroy(temp2, 0.4f);
        yield return new WaitForSeconds(0.7f);
        GameObject temp3 = Instantiate(warningEffect, warnigSpawnPoint.position, warnigSpawnPoint.rotation);
        Destroy(temp3, 0.4f);
        yield return new WaitForSeconds(0.7f);
    }

    // 데미지 패널을 표시하는 코루틴
    private IEnumerator DisplayDamagePanal()
    {
        Panal.SetActive(true);
        yield return new WaitForSeconds(damageDisplayTime);
        Panal.SetActive(false);
    }
}
