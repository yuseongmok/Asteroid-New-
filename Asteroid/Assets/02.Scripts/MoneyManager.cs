using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance { get; private set; } //static
    public int Money { get; private set; }

    public TextMeshProUGUI moneyText; // Money 수치를 표시할 UI Text


    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Money = 50;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    public void UpdateMoneyText()
    {
        if (moneyText == null)
        {
            // 만약 moneyText가 null이면 MoneyUI 오브젝트를 찾아서 할당합니다.
            GameObject moneyUIObject = GameObject.Find("MoneyUI");

            if (moneyUIObject != null)
            {
                moneyText = moneyUIObject.GetComponent<TextMeshProUGUI>();

            }
        }

        if (moneyText != null)
        {
            moneyText.text = "" + Money.ToString(); // Money 수치를 UI Text에 표시
        }
    }

    public void AddMoney(int amount)
    {
        Money += amount;
        UpdateMoneyText();
    }

    public void SpendMoney(int amount)
    {
        if (Money >= amount)
        {
            Money -= amount;
            UpdateMoneyText();
        }
    }

    public void Start()
    {
        UpdateMoneyText(); // 게임 시작 시 Money 수치를 UI Text에 표시
    }
}