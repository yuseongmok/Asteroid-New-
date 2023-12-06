using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // 플레이어 이동 속도
    public Camera mainCamera; // 카메라
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 플레이어 이동 입력 처리
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // 플레이어 이동 처리
        Vector2 moveDirection = new Vector2(horizontalInput, verticalInput).normalized;
        rb.velocity = moveDirection * moveSpeed;

        // 카메라 이동 처리
        Vector3 cameraPosition = new Vector3(transform.position.x, transform.position.y, mainCamera.transform.position.z);
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, cameraPosition, Time.deltaTime * 5f);
    }
}
