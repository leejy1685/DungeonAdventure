using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//플레이어의 컨디션을 관리하는 클래스
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
        //자동으로 스테미나 회복
        stamina.Add(stamina.passiveValue*Time.deltaTime); 

        //캐릭터 달리기 중일 시 스테미나를 소모
        if (controller.useRun && !UseStamina(controller.runStemina*Time.deltaTime))
        {
            //스테미나 전부 소모시 달리기 중지
            controller.useRun =  controller.Running(false);
        }
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

    public void StartPlayerCondition()
    {
        StartCoroutine(TakeOnDamage());
    }
    

    IEnumerator TakeOnDamage()
    {
        while (health.curValue > 0)
        {
            health.Subtract(health.passiveValue);

            yield return new WaitForSeconds(health.passiveValue);
        }
        
        GameManager.Instance.GameOver();
    }
        
    
    
    #region Item
    
    public void Heal(float amount)
    { 
        health.Add(amount);
    }
    
        
    #endregion
    
}
