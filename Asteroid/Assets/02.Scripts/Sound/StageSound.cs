using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSound : MonoBehaviour
{
    //사운드 매니저
    public SoundManager soundManager;

    // Start is called before the first frame update
    void Start()
    {
        soundManager.PlaySound(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
