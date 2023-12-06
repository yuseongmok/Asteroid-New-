using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuCanvas;      //메뉴창 오브젝트
    public Button GoStartButton;             //계속하기 버튼
    public Button RePlay;                   //다시하기 버튼
    public Button EndButton;                //게임종료 버튼

    //사운드 매니저
    public SoundManager soundManager;

    void Start()
    {
        pauseMenuCanvas.SetActive(false);
        GoStartButton.onClick.AddListener(GoStart);
        RePlay.onClick.AddListener(ReStart);
        EndButton.onClick.AddListener(EndGame);
    }

    void Update()
    {
        // Esc키 눌렸을 때
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Resume();
                
            }
            else
            {
                Pause();
               
            }
        }

        
    }

    // Esc키 누르면 메뉴창 띄우는 함수
    public void Resume()
    {
        pauseMenuCanvas.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Debug.Log("Game 계속하기");
        soundManager.PlaySound(3);
    }

    // 다시 Esc키 누르면 메뉴창 지우는 함수
    public void Pause()
    {
        pauseMenuCanvas.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Debug.Log("Game 일시정지");
        soundManager.PlaySound(3);
    }

    // 메뉴창에서 계속하기 버튼을 눌렸을 때 실행하는 코드
    public void GoStart()
    {
        Time.timeScale = 1f;
        pauseMenuCanvas.SetActive(false);
        GameIsPaused = false;
        Debug.Log("Game 계속하기");
        soundManager.PlaySound(3);
    }

    // 메뉴창에서 다시하기 버튼을 눌렸을 때 실행하는 코드
    public void ReStart()
    {
        SceneManager.LoadScene("Play");
        Time.timeScale = 1f;
        Debug.Log("Play Scene 다시시작");
    }

    public void EndGame()
    {
        Application.Quit();
        Debug.Log("게임종료");
        soundManager.PlaySound(3);
    }


}
