using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public interface IInteractable  //상호작용 가능한
{
    public string GetItemPrompt();// Item의 대한 설명
    public string GetInteractPrompt();//상호작용 방법에 대한 설명
    public void OnInteract();// 인터랙션 호출
}

public class ItemObject : MonoBehaviour,IInteractable
{
    public ItemData data;
    
    //아이템 설명
    public string GetItemPrompt()
    {
        string str = $"{data.displayName}\n{data.description}";
        return str;
    }
    
    //상호작용 방법에 대한 설명
    public string GetInteractPrompt()
    {
        switch (data.type)
        {
            case ItemType.Consumable:
                return "'E'키를 눌러 사용";
            case ItemType.Equipable:
                return "'E'키를 눌러 장착";
        }

        return "";
    }

    //상호작용 효과
    public void OnInteract()
    {
        //소모성 아이템
        if (data.type == ItemType.Consumable)
        {
            switch (data.consumable.type)
            {
                case ConsumableType.Health: //타입이 체력일 경우 회복
                    CharacterManager.Instance.Player.Condition.Heal(data.consumable.value);
                    break;
                case ConsumableType.Speed:  //타입이 속도일 경우 일정시간 동안 빨라짐
                    CharacterManager.Instance.Player.ItemBuff.MoveSpeedUp(data.consumable.value,
                        data.consumable.duration);
                    break;
                case ConsumableType.Jump:   //타입이 점프일 경우 일정시간 동안 점프 횟수 증가
                    CharacterManager.Instance.Player.ItemBuff.UpgradeJump(data.consumable.value,
                        data.consumable.duration);
                    break;
            }
        }

        //장비 아이템
        if (data.type == ItemType.Equipable)
        {   //아이템 장착
            CharacterManager.Instance.Player.Equipment.EquipNew(data);
        }
        
        //아이템 오브젝트 파괴
        Destroy(gameObject);
    }
}
