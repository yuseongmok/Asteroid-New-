using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance { get; private set; } //static
    public int Money { get; private set; }

    public TextMeshProUGUI moneyText; // Money ��ġ�� ǥ���� UI Text


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
            // ���� moneyText�� null�̸� MoneyUI ������Ʈ�� ã�Ƽ� �Ҵ��մϴ�.
            GameObject moneyUIObject = GameObject.Find("MoneyUI");

            if (moneyUIObject != null)
            {
                moneyText = moneyUIObject.GetComponent<TextMeshProUGUI>();

            }
        }

        if (moneyText != null)
        {
            moneyText.text = "" + Money.ToString(); // Money ��ġ�� UI Text�� ǥ��
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
        UpdateMoneyText(); // ���� ���� �� Money ��ġ�� UI Text�� ǥ��
    }
}