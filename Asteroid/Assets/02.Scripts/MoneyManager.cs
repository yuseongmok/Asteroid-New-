using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance { get; private set; }
    public int Money { get; private set; }

    [SerializeField]
    private TextMeshProUGUI moneyText; // Money 수치를 표시할 UI Text

    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        
    }

    

    private void UpdateMoneyText()
    {
        moneyText.text = "" + Money.ToString(); // Money 수치를 UI Text에 표시
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
        else
        {
            Debug.Log("Not enough money!");
        }
    }

    private void Start()
    {
        UpdateMoneyText(); // 게임 시작 시 Money 수치를 UI Text에 표시
    }
}
