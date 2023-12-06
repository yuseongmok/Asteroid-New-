using UnityEngine;
using System.Collections;


public class FadeInAndOut : MonoBehaviour
{
    public float fadeDuration = 2.0f; // 나타나고 사라지는 데 걸리는 시간
    
    private CanvasRenderer canvasRenderer;
    
   

    void OnEnable()
    {
        canvasRenderer = GetComponent<CanvasRenderer>();
        StartCoroutine(FadeInOut());
        
    }

    
    IEnumerator FadeInOut()
    {
        while (true)
        {
            // 이미지 서서히 나타나기
            for (float t = 0; t < fadeDuration; t += Time.deltaTime)
            {
                canvasRenderer.SetAlpha(t / fadeDuration);
                yield return null;
            }

            // 이미지 서서히 사라지기
            for (float t = fadeDuration; t > 0; t -= Time.deltaTime)
            {
                canvasRenderer.SetAlpha(t / fadeDuration);
                yield return null;
            }
        }
    }

    private void OnDisable()
    {
        StopCoroutine(FadeInOut());
    }


}
