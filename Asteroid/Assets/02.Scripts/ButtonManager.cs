using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Button Exitbutton;

    void Start()
    {
        
        Exitbutton.onClick.AddListener(exitbutton);
    }

    public void exitbutton()
    {
        Debug.Log("게임 종료");
        Application.Quit();
    }
}
