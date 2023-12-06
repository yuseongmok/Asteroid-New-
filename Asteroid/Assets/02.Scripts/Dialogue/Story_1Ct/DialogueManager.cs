using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public Text dialogueText;
    public List<string> dialogueList;
    private int currentIndex;
    private bool isTyping;
    
    public GameObject Ob;

    public Sprite[] images;
    public Image targetImage;
    public int ImageIndex = 0;

    public GameObject warring;
    //사운드 매니저
    public SoundManager soundManager;

    public Button SkipButton;

    private void Start()
    {
        currentIndex = 0;
        isTyping = false;
        dialogueText.text = "";
        Ob.SetActive(false);
        warring.SetActive(false);
        SkipButton.onClick.AddListener(Skip);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            Ob.SetActive(true);
            
            if (isTyping)
            {
                // 대화 텍스트가 아직 모두 표시되지 않았을 때, 텍스트를 바로 표시하고 애니메이션을 끝내기
                CompleteTyping();
                
            }
            else
            {
                ShowNextDialogue();
            }
        }


    }

    public void Skip()
    {
        SceneManager.LoadScene("Play");
    }

    void ShowNextDialogue()
    {
        if (currentIndex < dialogueList.Count)
        {
            StartCoroutine(TypeDialogue(dialogueList[currentIndex]));
            currentIndex++;
        }
        else
        {
            // 대화가 모두 표시되면 대화 UI를 비활성화하거나 다른 동작을 수행할 수 있음
            // 예를 들어, 대화 UI를 숨길 수 있습니다.
            // gameObject.SetActive(false);
        }
    }

    // 대사가 나오는 코드
    IEnumerator TypeDialogue(string text)
    {
        ImageIndex++; // 이미지 인덱스를 증가시키고 이미지를 변경합니다
        ChangeImage(); // 이미지를 변경하는 함수를 호출합니다
        
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in text.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.1f); // 한 글자씩 표시되는 시간을 조절
        }

        isTyping = false;
        
       
    }

    

    

    void ChangeImage()
    {
        // 이미지 체인지
        if (ImageIndex == 2 || ImageIndex == 8)
        {
            targetImage.sprite = images[1];
        }

        
        // warring 이미지와 사운드 나오는 구간
        if (ImageIndex == 5)
        {
            soundManager.PlaySound(0);
            warring.SetActive(true);
            targetImage.sprite = images[0];
        }

       if(ImageIndex == 11)
       {
            LoadingScene.LoadScene("Play");
       }
    }

    void CompleteTyping()
    {
        // 모든 글자를 한 번에 표시하고 애니메이션을 끝냄
        StopAllCoroutines();
        dialogueText.text = dialogueList[currentIndex - 1];
        isTyping = false;
    }
}
