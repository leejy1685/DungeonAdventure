using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public interface IInteractable
{
    public void SetActivePrompt(bool onOff);
    public string GetInteractPrompt();// UI에 표시할 정보
    public void OnInteract();// 인터랙션 호출
}

public class ItemObject : MonoBehaviour,IInteractable
{
    public ItemData data;

    public Transform canvas;
    public TextMeshProUGUI text;


    public void SetActivePrompt(bool onOff)
    {
        if (canvas == null)
            return;
        
        switch (data.type)
        {
            case ItemType.Consumable:
                text.text = "'E'를 눌러서 사용";
                break;
            case ItemType.Equipable:
                text.text = "'E'를 눌러서 장착";
                break;
        }
        
        canvas.gameObject.SetActive(onOff);
        canvas.LookAt(CharacterManager.Instance.Player.Controller.cameraContainer);
        
    }
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
                case ConsumableType.Speed:
                    CharacterManager.Instance.Player.ItemBuff.MoveSpeedUp(data.consumable.value,
                        data.consumable.duration);
                    break;
                case ConsumableType.Jump:
                    CharacterManager.Instance.Player.ItemBuff.UpgradeJump(data.consumable.value,
                        data.consumable.duration);
                    break;
            }
        }

        if (data.type == ItemType.Equipable)
        {
            CharacterManager.Instance.Player.Equipment.EquipNew(data);
        }
        
        Destroy(gameObject);
    }


    
}
