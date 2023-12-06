using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotUI : MonoBehaviour
{
    public List<Button> buttonList; // Button을 저장할 List 변수
    public GameObject uiPanel; // 활성화할 UI 창
    public SoundManager soundManager;

    private void Start()
    {
        // Button들을 List에 추가
        buttonList = new List<Button>(GetComponentsInChildren<Button>());

        // 각 Button에 클릭 이벤트를 연결
        foreach (Button button in buttonList)
        {
            button.onClick.AddListener(() => ToggleUIPanel());
        }

        // UI 패널을 초기에 비활성화
        uiPanel.SetActive(false);
    }

    // UI 패널을 활성화/비활성화하는 함수
    void ToggleUIPanel()
    {
        uiPanel.SetActive(!uiPanel.activeSelf);
        soundManager.PlaySound(0);
    }
}
