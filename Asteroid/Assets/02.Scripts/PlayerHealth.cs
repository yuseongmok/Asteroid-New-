using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // 최대 체력
    public int currentHealth; // 현재 체력

    public Slider healthBar; // HP 바 UI
    public GameObject Panal;
    MonsterController Enemy;
    
    // 데미지 표시 시간
    private float damageDisplayTime = 0.2f;

    private void Start()
    {
        Panal.SetActive(false);
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            
            
            Debug.Log("공격에 맞음");
        }
    }

    


    public void TakeDamage1(int damage)
    {
       
        currentHealth -= damage;
        StartCoroutine(DisplayDamagePanal()); // 데미지 패널을 표시하는 코루틴 시작

        healthBar.value = currentHealth;

        if (currentHealth == 0)
        {
            SceneManager.LoadScene("Stage");
        }
    }

    // 데미지 패널을 표시하는 코루틴
    private IEnumerator DisplayDamagePanal()
    {
        Panal.SetActive(true);
        yield return new WaitForSeconds(damageDisplayTime);
        Panal.SetActive(false);
    }
}
