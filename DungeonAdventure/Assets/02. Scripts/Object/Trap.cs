using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private TrapIndicator trapIndicator;
    private Ray ray;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private float checkDistance;

    private float lastCheckTime;
    [SerializeField] private float checkRate;
    
    private void Start()
    {
        trapIndicator = GameManager.Instance.uiManager.getTrapIndicator();
        ray = new Ray(transform.position, transform.forward);
        targetLayer = LayerMask.GetMask("Player");
    }

    private void Update()
    {
        CheckTrap();
    }

    //함정에 걸렸는지 확인하는 메서드
    private void CheckTrap()
    {
        //배치에 어려움이 있을거 같아 확인용
        Debug.DrawRay(transform.position,transform.forward * checkDistance, Color.black );
        
        //시간마다 체크
        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;

            if (Physics.Raycast(ray, checkDistance, targetLayer))
            {
                trapIndicator.Flash();
            }
            
        }
    }
}
