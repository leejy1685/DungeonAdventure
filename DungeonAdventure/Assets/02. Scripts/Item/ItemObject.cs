using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public interface IInteractable  //상호작용 가능한
{
    public void SetActivePrompt(bool onOff);//아이템 오브젝트에서 Text표시
    public string GetInteractPrompt();// UI에 표시할 정보
    public void OnInteract();// 인터랙션 호출
}

public class ItemObject : MonoBehaviour,IInteractable
{
    public ItemData data;

    //오브젝트 UI
    public Transform canvas;
    public TextMeshProUGUI text;

    
    // 이 병신같은 코드 없애고 UI로 기능 할당하기
    public void SetActivePrompt(bool onOff)
    {
        //아이템 사용 시 오류 발생 방지
        if (canvas == null)
            return;
        
        //아이템 타입에 따른 사용 방법
        switch (data.type)
        {
            case ItemType.Consumable:
                text.text = "'E'를 눌러서 사용";
                break;
            case ItemType.Equipable:
                text.text = "'E'를 눌러서 장착";
                break;
        }
        
        //유저를 보기(유저 카메라)
        canvas.LookAt(CharacterManager.Instance.Player.Controller.cameraContainer);
        canvas.gameObject.SetActive(onOff);
    }
    
    //아이템 설명
    public string GetInteractPrompt()
    {
        string str = $"{data.displayName}\n{data.description}";
        return str;
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
