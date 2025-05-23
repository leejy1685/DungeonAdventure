using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//유저의 아이템 버프 효과를 관리하는 클래스
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

    //속도 증가 메서드
    public void MoveSpeedUp(float value,float duration)
    {
        if (SpeedUpCoroutine != null)
        {
            SpeedUpCoroutine = null;
        }
        SpeedUpCoroutine = StartCoroutine(SpeedUp( value, duration));
    }
    
    //속도 증가 코루틴
    IEnumerator SpeedUp(float value,float duration)
    {
        controller.UpdateMoveSpeed(value);
        
        yield return new WaitForSeconds(duration);

        if (controller.useRun)  //달리기 중 일 때
        {
            controller.UpdateMoveSpeed();
            controller.Running(true);
        }
        else
        {
            controller.UpdateMoveSpeed();
        }
    }

    
    //더블 점프 버프 (value값에 따라 더 가능)
    [Header("DoubleJump")] 
    private Coroutine DoubleJumpCoroutine;
    
    //점프 횟수 증가 메서드
    public void UpgradeJump(float value,float duration)
    {
        if (DoubleJumpCoroutine != null)
        {
            DoubleJumpCoroutine = null;
        }
        DoubleJumpCoroutine = StartCoroutine(DoubleJump( value, duration));
    }

    //점프 횟수 증가 코루틴
    IEnumerator DoubleJump(float value, float duration)
    {
        controller.jumpCount = (int)value;
        yield return new WaitForSeconds(duration);
        controller.jumpCount = 1;
    }

    #endregion
}
