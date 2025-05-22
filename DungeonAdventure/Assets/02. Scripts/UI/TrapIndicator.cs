using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrapIndicator : MonoBehaviour
{
    private Coroutine coroutine;
    
    private Image image;
    [SerializeField]private float flashSpeed;

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
            a -= (startAlpha / flashSpeed) * Time.deltaTime;
            image.color = new Color(1f, 105f / 255f, 105f / 255f,a);
            yield return null;  //프레임 당 호출
        }

        image.enabled = false;
    }
    
    
}
