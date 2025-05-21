using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBuff : MonoBehaviour
{
    private PlayerController controller;
    private void Awake()
    {
        controller = GetComponent<PlayerController>();
    }
    
    #region ItemBuff

    
    //속도 증가 버프
    [Header("SpeedUp")]
    private Coroutine SpeedUpCoroutine;

    public void MoveSpeedUp(float value,float duration)
    {
        if (SpeedUpCoroutine != null)
        {
            SpeedUpCoroutine = null;
        }
        SpeedUpCoroutine = StartCoroutine(SpeedUp( value, duration));
    }
    
    IEnumerator SpeedUp(float value,float duration)
    {
        controller.defSpeed *= value;
        controller.moveSpeed = controller.defSpeed;
        
        yield return new WaitForSeconds(duration);
        
        controller.defSpeed /= value;
        controller.moveSpeed = controller.defSpeed;
    }

    
    //더블 점프 버프 (value값에 따라 더 가능)
    [Header("DoubleJump")] 
    private Coroutine DoubleJumpCoroutine;
    
    public void UpgradeJump(float value,float duration)
    {
        if (DoubleJumpCoroutine != null)
        {
            DoubleJumpCoroutine = null;
        }
        DoubleJumpCoroutine = StartCoroutine(DoubleJump( value, duration));
    }

    IEnumerator DoubleJump(float value, float duration)
    {
        controller.jumpCount = (int)value;
        yield return new WaitForSeconds(duration);
        controller.jumpCount = 1;
    }

    #endregion
}
