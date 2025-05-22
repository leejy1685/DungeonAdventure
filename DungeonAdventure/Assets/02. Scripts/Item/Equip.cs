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
    public ItemData itemData;
    public Equipable[] equipables;
    
    
    public void EquipEffect()
    {
        foreach (Equipable equipable in equipables)
        {
            switch (equipable.equipType)
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
