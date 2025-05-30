using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpownBlock : MonoBehaviour
{
    [SerializeField] private ItemData itemData;
    
    private void OnCollisionEnter(Collision other)
    {
        //블록을 밟았을 때 아이템 생성
        if (other.gameObject.CompareTag("Player") && other.transform.position.y > transform.position.y)
        {
            Instantiate(itemData.dropPrefab, transform.position + Vector3.up * 2f, Quaternion.identity);
        }
    }
}
