using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//유저의 모든 정보를 관리하는 메서드
public class Player : MonoBehaviour
{
    //전부 프로퍼티로 수정
    public PlayerController Controller { get; private set; }
    public PlayerCondition Condition{ get; private set; }
    public Interaction Interaction{ get; private set; }
    public ItemBuff ItemBuff{ get; private set; }
    public Equipment Equipment{ get; private set; }
    
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
