using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class WaveSystem : MonoBehaviour
{
    //Wave(wave 구현)
    public Button waveButton;              // Wave를 시작할 버튼
    public TextMeshProUGUI waveText;       // Wave 수를 표시할 UI Text 요소
    public List<GameObject> enemyPrefabs;  // 각 웨이브마다 나오는 Enemy 패턴
    public Transform spawnPoint;           // Enemy가 생성될 위치
    public Image imageToControl;        // 숨기거나 표시할 이미지

    private int currentWave = 0;            // 현재 Wave 번호
    private int currentEnemyIndex = 0;      // 현재 Enemy 프리팹 인덱스
    private bool isWaveInProgress = false;  // Wave 진행 중인지 여부
    public GameObject Enemy;

    //Player(메인 주포)
    public Transform pivot;               // 피봇 포인트
    public GameObject bulletPrefab;       // 총알 프리팹
    public Transform firePoint;           // 총알 발사 위치
    public float rotationSpeed = 5.0f;    // 피봇 회전 속도
    public float minAngle = -45.0f;       // 최소 회전 각도
    public float maxAngle = 45.0f;        // 최대 회전 각도
    public float timer = 0.0f;
    private bool TowerOnOff = true;
    public Button ToswerOn;

    //사운드 매니저
    public SoundManager soundManager;

    //Slot UI
    public GameObject UITowerButtonGroup;
    public GameObject ShopUI;

    private void Start()
    {
        //Wave 코드
        waveButton.onClick.AddListener(StartWave);  // 버튼 클릭 이벤트 리스너 추가
        ToswerOn.onClick.AddListener(TowerOn);
        UpdateWaveText();  // 초기 Wave 번호 표시
        soundManager.PlaySound(0);
        MoneyManager.Instance.AddMoney(50);

    }
    // Wave Text 
    private void UpdateWaveText()
    {
        //Wave 코드
        if (currentEnemyIndex < enemyPrefabs.Count + 1)
        {
            waveText.text = "Wave " + currentWave.ToString();  // UI Text 업데이트
        }
        else
        {
            waveText.text = "업데이트 중";  // 다음 GameObject가 없을 때 "업데이트 중"으로 표시
        }
    }

    private void Update()
    {
        // Wave 진행될 때
        if (isWaveInProgress)
        {
            //Wave
            
            // Wave 진행 중에는 버튼을 비활성화
            waveButton.interactable = false;

            // Wave 진행 중인지 체크하고, 모든 Enemy가 없어지면 Wave 종료
            if (CheckWaveComplete())
            {
                isWaveInProgress = false;
                waveButton.interactable = true;  // Wave가 종료되면 버튼을 활성화
                UITowerButtonGroup.SetActive(true);
                
                
                Destroy(Enemy);
            }

            //Player(메인주포 코드)

            // 피봇을 마우스 위치로 회전시킴
            RotatePivotToMouse();

            timer += Time.deltaTime;

            // 마우스 클릭으로 총알 발사
            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
            }
        }
        else
        {
            // Wave가 종료되면 버튼을 활성화
            waveButton.interactable = true;
        }

        // Esc키 누르면 Player 공격 활성화,비활성화
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(TowerOnOff)
            {
                TowerOnOff = false;
            }
            else
            {
                TowerOnOff = true;
            }
        }
    }

    public void TowerOn()
    {
        TowerOnOff = true;
    }

    // Wave를 시작하는 함수
    private void StartWave()
    {
        //Wave
        isWaveInProgress = true;  // Wave 진행 중으로 설정
        UITowerButtonGroup.SetActive(false);
        waveButton.interactable = false;
        ShopUI.SetActive(false); //ShopUi 창 비활성화
        soundManager.PlaySound(3);
        soundManager.PlaySound(1);

        if (currentEnemyIndex < enemyPrefabs.Count)
        {
            // 순서대로 Enemy 프리팹을 선택하여 생성
            GameObject enemy = Instantiate(enemyPrefabs[currentEnemyIndex], spawnPoint.position, Quaternion.identity);
            Enemy = enemy;
            currentEnemyIndex++;
            soundManager.StopSound(0);
            if(currentEnemyIndex == 5)
            {
                EndScene();
            }
        }
        else
        {
            Debug.Log("모든 Enemy 프리팹이 생성되었습니다.");
        }
        currentWave++;  // Wave 번호 증가
        UpdateWaveText();  // Wave 번호 업데이트
        HideImage();
    }

    // Wave가 완료되었는지 확인하는 함수
    private bool CheckWaveComplete()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        return enemies.Length == 0;
        

    }

    //웨이브 버튼 이미지를 숨김
    private void HideImage()
    {
        if (imageToControl != null)
        {
            imageToControl.enabled = false;
        }
    }

    //웨이브 버튼 이미지 다시 보이기
    private void ShowImage()
    {
        if (imageToControl != null)
        {
            imageToControl.enabled = true;
        }
    }

    //Player(메인주포 코드)
    void RotatePivotToMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        Vector3 direction = (mousePosition - pivot.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // 회전 각도를 제한
        angle = Mathf.Clamp(angle, minAngle, maxAngle);

        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        pivot.rotation = Quaternion.Slerp(pivot.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    // Player 공격 함수
    void Shoot()
    {
        if(TowerOnOff == true)
        {
            if (timer >= 0.33f)
            {
                soundManager.PlaySound(4);
                // 총알을 발사 위치에서 생성
                Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                timer = 0.0f;
            }
        }
       
    }

    public void EndScene()
    {
        SceneManager.LoadScene("Story02");
    }
}


