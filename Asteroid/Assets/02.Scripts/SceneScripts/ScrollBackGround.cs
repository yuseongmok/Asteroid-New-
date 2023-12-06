using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackGround : MonoBehaviour
{
    public float scrollSpeed;                                               // 돌아가는 속도 선언
    public Renderer rend;                                                   // 렌드(사진) 선언

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();                                    // 렌드에 렌더링 가져오기
    }

    // Update is called once per frame
    void Update()
    {
        float offset = Time.time * scrollSpeed;                             //offset에 속도 *시간 (Time.deltatime을 쓰지 않는 이유는 배경 같은 경우에는 속도가 중요하지 않기 때문이다) 대입
        rend.material.mainTextureOffset = new Vector2(offset, 0);           // 렌드에 메터리얼의 텍스쳐에  X축에 Offset 대입
    }
}

