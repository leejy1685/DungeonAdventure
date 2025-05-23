using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipType
{
    Speed,
    Jump
}

[System.Serializable]
public class Equipable
{
    public EquipType equipType;
    public float value;
}

public class Equip : MonoBehaviour
{
    [SerializeField]private ItemData itemData;
    public ItemData ItemData
    {
        get { return itemData; }
    }
    [SerializeField]private Equipable[] equipables;
    
    //아이템 장착 효과
    public void EquipEffect()
    {
        foreach (Equipable equipable in equipables)
        {
            switch (equipable.equipType)    //장착 타입에 따른 효과 업
            {
                case EquipType.Speed:
                    CharacterManager.Instance.Player.Controller.defSpeed += equipable.value;
                    CharacterManager.Instance.Player.Controller.UpdateMoveSpeed();
                    break;
                case EquipType.Jump:
                    CharacterManager.Instance.Player.Controller.jumpPower += equipable.value;
                    break;
            }
        }
    }
    
    //장비 해제 시 
    public void UnEquipEffect()
    {
        foreach (Equipable equipable in equipables)
        {
            switch (equipable.equipType)
            {
                case EquipType.Speed:
                    CharacterManager.Instance.Player.Controller.defSpeed -= equipable.value;
                    break;
                case EquipType.Jump:
                    CharacterManager.Instance.Player.Controller.jumpPower -= equipable.value;
                    break;
            }
        }
    }
}
