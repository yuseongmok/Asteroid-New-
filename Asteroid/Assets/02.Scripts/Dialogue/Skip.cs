using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Skip : MonoBehaviour
{
    public Button SkipButton;
    public GameObject Tutorial;
    
    public void Start()
    {
        SkipButton.onClick.AddListener(ButtonClick);
    }

    public void ButtonClick()
    {
        if (Tutorial != null)
        {
            Tutorial.SetActive(false); 
        }
        
    }

}
