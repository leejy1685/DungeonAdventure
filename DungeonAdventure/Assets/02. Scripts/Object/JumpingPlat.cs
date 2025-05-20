using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingPlat : MonoBehaviour
{
    [SerializeField]private float JumpPower;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Rigidbody rigidbody = other.gameObject.GetComponent<Rigidbody>();
            rigidbody.AddForce(other.transform.up* JumpPower,ForceMode.Impulse);
        }
    }
}
