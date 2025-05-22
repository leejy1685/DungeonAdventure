using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Equipable,
    Consumable
}
public enum ConsumableType
{
    Health,
    Speed,
    Jump
}

[System.Serializable]
public class ItemDataConsumable
{
    public ConsumableType type;
    public float duration;
    public float value;
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;
    public ItemType type;
    public GameObject dropPrefab;
    
    [Header("Consumable")]
    public ItemDataConsumable consumable;
    
    [Header("Equip")]
    public GameObject equipPrefab;
}
