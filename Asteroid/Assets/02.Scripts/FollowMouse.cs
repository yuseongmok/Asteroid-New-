using UnityEngine;
using UnityEngine.UI;

public class FollowMouse : MonoBehaviour
{
    public Texture2D cursorTexture; // Inspector에서 할당할 커서 이미지
    public Vector2 hotSpot = Vector2.zero; // 커서 이미지의 중심 위치

    void Start()
    {
        // CursorMode.ForceSoftware를 사용하여 하드웨어 가속을 사용하지 않도록 설정
        Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.ForceSoftware);
    }

    void Update()
    {
        // 마우스 위치를 가져오기
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane; // 카메라와의 거리를 설정

        // 마우스 위치를 월드 좌표로 변환
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // 이미지를 마우스 위치로 이동
        transform.position = worldMousePosition;
    }
}
