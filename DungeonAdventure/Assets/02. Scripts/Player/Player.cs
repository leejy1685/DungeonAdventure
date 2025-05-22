using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController Controller;
    public PlayerCondition Condition;
    public Interaction Interaction;
    public ItemBuff ItemBuff;
    public Equipment Equipment;
    
    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        Controller = GetComponent<PlayerController>();
        Condition = GetComponent<PlayerCondition>();
        Interaction = GetComponent<Interaction>();
        ItemBuff = GetComponent<ItemBuff>();
        Equipment = GetComponent<Equipment>();
    }
    
    
}
