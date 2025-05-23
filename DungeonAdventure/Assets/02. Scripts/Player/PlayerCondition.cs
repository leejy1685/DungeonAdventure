using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//플레이어의 컨디션을 관리하는 메서드
public class PlayerCondition : MonoBehaviour
{
    private PlayerController controller;
    public UICondition uiCondition;
    Condition health { get { return uiCondition.health; } }
    Condition stamina { get { return uiCondition.stamina; } }


    private void Awake()
    {
        controller = CharacterManager.Instance.Player.Controller;
        //그냥 GetComponent<PlayerController>();와 다른점을 알고 싶긴함.
    }

    private void Update()
    {
        //게임 시작 전까지 실행하지 않음
        if (!GameManager.Instance.isPlaying)
            return;
        
        //체력 소모를 코루틴으로
        health.Subtract(health.passiveValue*Time.deltaTime);    //자동으로 체력 달기 시간제한
        stamina.Add(stamina.passiveValue*Time.deltaTime);       //자동으로 스테미나 회복

        //캐릭터 달리기 중일 시 스테미나를 소모
        if (controller.useRun && !UseStamina(controller.runStemina*Time.deltaTime))
        {
            //스테미나 전부 소모시 달리기 중지
            controller.useRun =  controller.Running(false);
        }

         
        //비용이 높은 이유는 알고 있으나 해결 방법을 모르겠음.
        Die();
    }
    

    public void Die()
    {
        if (health.curValue <= 0)
            GameManager.Instance.GameOver();
    }
    
    public bool UseStamina(float amount)
    {
        if (stamina.curValue - amount < 0)
        {
            return false;
        }
        stamina.Subtract(amount);
        return true;
    }
        
    
    
    #region Item
    
    public void Heal(float amount)
    { 
        health.Add(amount);
    }
    
        
    #endregion
    
}
