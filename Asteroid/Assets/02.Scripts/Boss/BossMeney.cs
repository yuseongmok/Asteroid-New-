using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossMeney : MonoBehaviour
{
    public Button WaveButton;
    public GameObject Boss;
    // Start is called before the first frame update
    void Start()
    {
        Boss.SetActive(false);
        
        WaveButton.onClick.AddListener(StartWave);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void StartWave()
    {
        Boss.SetActive(true);
    }
}
