using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    public UICondition uiCondition;
    Condition health { get { return uiCondition.health; } }
    Condition stamina { get { return uiCondition.stamina; } }
    
    private void Update()
    {
        health.Subtract(health.passiveValue*Time.deltaTime);
        stamina.Add(stamina.passiveValue*Time.deltaTime);
    }

    public void Die()
    {
        //게임 종료
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
        
    public void TakePhysicalDamage(int damageAmount) 
    { 
        health.Subtract(damageAmount);
    }
        
    #endregion
    
}
