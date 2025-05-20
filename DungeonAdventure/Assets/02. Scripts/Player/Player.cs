using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController PlayerController;
    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        PlayerController = GetComponent<PlayerController>();
    }
    
    
}
