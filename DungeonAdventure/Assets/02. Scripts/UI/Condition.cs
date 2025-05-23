using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//컨디션을 다루는 클래스
public class Condition : MonoBehaviour
{
    public float curValue;  //현재 값
    public float maxValue;  //최대 값
    public float startValue;    //시작 값
    public float passiveValue;  //패시브 값
    public Image uiBar; //UI에 표시

    //시작 값 셋팅
    public void SetValue()
    {
        curValue = startValue;
    }

    //상시로 UI에 표시
    private void Update()
    {
        uiBar.fillAmount = GetPercentage();
    }

    public void Add(float amount)
    {
        // 둘 중의 작은 값 (ex. maxValue보다 커지면 maxValue)
        curValue = Mathf.Min(curValue + amount, maxValue);
    }

    public void Subtract(float amount)
    {
        // 둘 중의 큰 값 (ex. 0보다 작아지면 0)
        curValue = Mathf.Max(curValue - amount, 0.0f);
    }

    public float GetPercentage()
    {
        return curValue / maxValue;
    }
}
