using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UICondition : MonoBehaviour
{
    public Condition health;
    public Condition stamina;

    private void Awake()
    {
        Condition[] conditions = GetComponentsInChildren<Condition>();
        health = conditions[0];
        stamina = conditions[1];
    }

    private void Start()
    {
        CharacterManager.Instance.Player.Condition.uiCondition = this;
    }

    public void SetConditon()
    {
        health.SetValue();
        stamina.SetValue();
    }
    
}
