using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public string GetInteractPrompt();// UI에 표시할 정보
    public void OnInteract();// 인터랙션 호출
}

public class ItemObject : MonoBehaviour,IInteractable
{
    public ItemData data;
    public string GetInteractPrompt()
    {
        string str = $"{data.displayName}\n{data.description}";
        return str;
    }

    public void OnInteract()
    {
        if (data.type == ItemType.Consumable)
        {
            switch (data.consumable.type)
            {
                case ConsumableType.Health:
                    CharacterManager.Instance.Player.Condition.Heal(data.consumable.value);
                    break;
                case ConsumableType.Speed :
                    CharacterManager.Instance.Player.Controller.MoveSpeedUp(data.consumable.value,data.consumable.duration);
                    break;
            }
            
        }
        Destroy(gameObject);
    }


    
}
