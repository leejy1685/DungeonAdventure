using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingPlat : MonoBehaviour
{
    [SerializeField]private float JumpPower;
    private void OnCollisionEnter(Collision other)
    {
        //블록을 밟았을 때 캐릭터 위로 날려보내기
        if (other.gameObject.CompareTag("Player"))
        {
            Rigidbody rigidbody = other.gameObject.GetComponent<Rigidbody>();
            rigidbody.AddForce(other.transform.up* JumpPower,ForceMode.Impulse);
        }
    }
}
