using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    // ���� ü�� ���� �ڵ�
    public int maxHp = 5000;        // �ִ� ü��
    public int currentHp;           // ���� ü��
    public Slider HpBar;            // Hp �� UI

    // ���� ���� ���
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

    // ������ �Ծ��� �� ȿ��
    public GameObject Panal;
    private float damageDisplayTime = 0.2f; // ������ ǥ�� �ð�

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

    void OnPattern()    // ���� ���� �����ϴ� �Լ�
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




    public void TakeDamage(int damage)   // ������ ������ �޴� ����
    {
        currentHp -= damage;            // ��������ŭ ���� ü�� ����
        HpBar.value = currentHp;        // Slider �پ��
        StartCoroutine(DisplayDamagePanal()); // ������ �г��� ǥ���ϴ� �ڷ�ƾ ����

        if (currentHp <= 500)
        {

        }
        if (currentHp <= 0)
        {
            Destroy(gameObject);
           
        }

    }

    void Attack()   // BossAttack ������ BossAttackSpawn�� ����
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

    // ������ �г��� ǥ���ϴ� �ڷ�ƾ
    private IEnumerator DisplayDamagePanal()
    {
        Panal.SetActive(true);
        yield return new WaitForSeconds(damageDisplayTime);
        Panal.SetActive(false);
    }
}
