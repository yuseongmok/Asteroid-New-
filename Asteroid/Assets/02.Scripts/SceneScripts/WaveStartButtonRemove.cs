using UnityEngine;
using UnityEngine.UI;

public class WaveStartButtonRemove : MonoBehaviour
{
    public Button button; // 버튼 GameObject에 연결할 버튼 컴포넌트
    public Image image;   // 이미지를 표시할 GameObject에 연결할 이미지 컴포넌트

    private bool isButtonActive = true; // 버튼 상태를 추적

    void Start()
    {
        // 버튼 클릭 이벤트에 함수 연결
        button.onClick.AddListener(ToggleButtonState);
    }

    void ToggleButtonState()
    {
        isButtonActive = !isButtonActive;

        // 버튼 상태에 따라 버튼 활성화/비활성화 설정
        button.interactable = isButtonActive;

        // 버튼 상태에 따라 이미지 표시/숨김 설정
        image.gameObject.SetActive(isButtonActive);
    }
}