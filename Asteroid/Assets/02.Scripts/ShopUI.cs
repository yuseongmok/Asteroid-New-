using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopUI : MonoBehaviour
{
    public List<Button> buttons;
    public Sprite[] images;
    public Image targetImage;
    public int ImageIndex = 0;

    public TextMeshProUGUI TowerName;
    public TextMeshProUGUI TowerPower;
    public TextMeshProUGUI TowerSpeed;
    public TextMeshProUGUI TowerExplanation;
    
    
    public TextMeshProUGUI NoMoney;
    public Button tower;
    public List<GameObject> Tower;
    public List<Transform> TowerSlots; // Change the variable to a list of Transform
    public List<bool> IsSlotOccupied; // To keep track of whether a tower is installed in each slot
    public List<int> towerCosts;

    public Button ShopUIX;
    public GameObject ShopUI_1;
    //사운드 매니저
    public SoundManager soundManager;
    void Start()
    {
        NoMoney.enabled = false;
        // Initialize the IsSlotOccupied list
        IsSlotOccupied = new List<bool>(new bool[TowerSlots.Count]);

        // Attach click events to the buttons
        foreach (Button btn in buttons)
        {
            btn.onClick.AddListener(() => OnButtonClick(buttons.IndexOf(btn)));
        }
        tower.onClick.AddListener(TowerInstantiate);
        ShopUIX.onClick.AddListener(UIX);
    }

    
    private void TowerInstantiate()
    {
        soundManager.PlaySound(0);
        int towerCost = towerCosts[ImageIndex];

        int currentMoney = MoneyManager.Instance.Money;

        if (currentMoney >= towerCost)
        {
            // Find the first available slot
            int slotIndex = FindEmptySlot();

            if (slotIndex != -1)
            {
                
                // Instantiate the tower at the selected slot
                Instantiate(Tower[ImageIndex], TowerSlots[slotIndex].position, Quaternion.identity);
                
                IsSlotOccupied[slotIndex] = true;

                // Deduct the tower cost from the money
                MoneyManager.Instance.SpendMoney(towerCost);
                soundManager.PlaySound(1);
            }
            else
            {
                Debug.Log("타워 슬롯이 없어요");
            }
        }
        else
        {
            StartCoroutine(ShowNoMoneyText());
            Debug.Log("돈이 부족해요");
        }
    }

    private int FindEmptySlot()
    {
        for (int i = 0; i < IsSlotOccupied.Count; i++)
        {
            if (!IsSlotOccupied[i])
            {
                return i;
            }
        }
        return -1; // No available slot
    }

    private void OnButtonClick(int index)
    {
        soundManager.PlaySound(0);
        ImageIndex = index;
        targetImage.sprite = images[index];

        switch (ImageIndex)
        {
            case 0:
                TowerName.text = "레일건";
                TowerPower.text = "공격력 50";
                TowerSpeed.text = "보통";
                TowerExplanation.text = "공격력이 강한 레이저를 발사";
                break;

            case 1:
                TowerName.text = "더블배럴";
                TowerPower.text = "공격력 200";
                TowerSpeed.text = "느림";
                TowerExplanation.text = "산탄의 발사체를 발사";
                break;
            case 2:
                TowerName.text = "아이스";
                TowerPower.text = "공격력 100";
                TowerSpeed.text = "보통";
                TowerExplanation.text = "공격시 적 이동속도 70%감소";
                break;
            case 3:
                TowerName.text = "마그마";
                TowerPower.text = "공격력 500";
                TowerSpeed.text = "느림";
                TowerExplanation.text = "연속 공격을 한다";
                break;

            case 4:
                TowerName.text = "라이트닝";
                TowerPower.text = "공격력 5";
                TowerSpeed.text = "매우 빠름";
                TowerExplanation.text = "레이저 번개를 쏜다";
                break;

            default:

                break;
        }
    }

    private IEnumerator ShowNoMoneyText()
    {
        NoMoney.enabled = true;
        yield return new WaitForSeconds(0.8f); // 필요에 따라 지속 시간을 조절할 수 있습니다.
        NoMoney.enabled = false;
    }

    public void UIX()
    {
        ShopUI_1.SetActive(false);
        soundManager.PlaySound(0);
    }
}
