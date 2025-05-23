using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//아이템 타입을 나타내는 이넘 타입
public enum ItemType
{
    Equipable,
    Consumable
}

//소모 아이템의 타입을 나타내는 이넘 타입
public enum ConsumableType
{
    Health,
    Speed,
    Jump
}

//직렬화 가능한 소모 아이템 정보 클래스
[System.Serializable]
public class ItemDataConsumable
{
    public ConsumableType type;
    public float duration;
    public float value;
}

//Asset폴더에서 생성 가능한 아이템 데이터 클래스
[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    //아이템 기본 정보
    [Header("Info")]
    public string displayName;
    public string description;
    public ItemType type;
    public GameObject dropPrefab;
    
    //소모 아이템 정보
    [Header("Consumable")]
    public ItemDataConsumable consumable;
    
    //장비 아이템의 장착 프리펩
    [Header("Equip")]
    public GameObject equipPrefab;
}
