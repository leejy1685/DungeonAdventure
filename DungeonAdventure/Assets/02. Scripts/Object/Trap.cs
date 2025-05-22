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
        trapIndicator = GameObject.Find("TrapIndicator").GetComponent<TrapIndicator>();
        ray = new Ray(transform.position, transform.forward);
        targetLayer = LayerMask.GetMask("Player");
    }

    private void Update()
    {
        CheckTrap();
    }

    private void CheckTrap()
    {
        Debug.DrawRay(transform.position,transform.forward * checkDistance, Color.black );
        
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
