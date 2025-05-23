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
    
    private void Awake()
    {
        trapIndicator = FindAnyObjectByType<TrapIndicator>();
        ray = new Ray(transform.position, transform.forward);
        targetLayer = LayerMask.GetMask("Player");
    }

    private void Update()
    {
        CheckTrap();
    }

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
                GameManager.Instance.uiManager.StartTrap();
            }
            
        }
    }
}
