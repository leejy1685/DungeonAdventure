using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    private PlayerController controller;
    public UICondition uiCondition;
    Condition health { get { return uiCondition.health; } }
    Condition stamina { get { return uiCondition.stamina; } }


    private void Awake()
    {
        controller = CharacterManager.Instance.Player.Controller;
    }

    private void Update()
    {
        health.Subtract(health.passiveValue*Time.deltaTime);
        stamina.Add(stamina.passiveValue*Time.deltaTime);

        if (controller.useRun && !UseStamina(controller.runStemina*Time.deltaTime))
        {
            controller.useRun =  controller.Running(false);
        }

        Die();
    }

    public void Die()
    {
        if(GameManager.Instance.isPlaying && health.curValue<=0)
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
