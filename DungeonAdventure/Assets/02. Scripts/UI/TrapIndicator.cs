using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//함정UI
public class TrapIndicator : MonoBehaviour
{
    //함정 코루틴
    private Coroutine coroutine;
    
    //함정에 사용되는 이미지와 유지 시간
    private Image image;
    [SerializeField]private float flashTime;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    //화면 가리기 함정
    public void Flash()
    {
        if (coroutine != null)
        {
            coroutine = null;
        }

        image.enabled = true;
        image.color = new Color(1f, 105f / 255f, 105f / 255f);
        coroutine = StartCoroutine(FadeAway());
    }

    //점차 흐려짐
    IEnumerator FadeAway()
    {
        float startAlpha = 1f;
        float a = startAlpha;

        while (a > 0)
        {
            a -= (startAlpha / flashTime) * Time.deltaTime;
            image.color = new Color(1f, 105f / 255f, 105f / 255f,a);
            yield return null;  //프레임 당 호출
        }

        image.enabled = false;
    }
    
    
}
