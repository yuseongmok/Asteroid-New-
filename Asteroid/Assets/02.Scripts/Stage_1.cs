using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage_1 : MonoBehaviour
{
    public GameObject stage1_UI;
    private bool Range = false;

   
    void Update()
    {
        if(Range == true)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                LoadNextScene();
            }
        }
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene("Story01");
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            stage1_UI.SetActive(true);
            Range = true;
        }
        
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            stage1_UI.SetActive(false);
            Range = false;
        }
    }
}
