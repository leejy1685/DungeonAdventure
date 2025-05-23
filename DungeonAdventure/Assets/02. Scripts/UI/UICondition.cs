using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//컨디션 정보를 표시하는 UI
public class UICondition : MonoBehaviour
{
    public Condition health;
    public Condition stamina;

    private void Awake()
    {
        Condition[] conditions = GetComponentsInChildren<Condition>();
        health = conditions[0];
        stamina = conditions[1];
        
        CharacterManager.Instance.Player.Condition.uiCondition = this;
    }

    //시작 시 값 넣어주기
    public void SetConditon()
    {
        health.SetValue();
        stamina.SetValue();
    }
    
}
