using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//장착 장비를 관리하는 클래스
public class Equipment : MonoBehaviour
{
    public Equip curEquip;
    public Transform firstPersonEquipPosition;
    public Transform thirdPersonEquipPosition;
    public Transform dropPosition;
    
    private PlayerController controller;
    private CameraController camera;
    
    void Start()
    {
        controller = CharacterManager.Instance.Player.Controller;
        camera = Camera.main.GetComponent<CameraController>();
    }
    
    //장비 장착
    public void EquipNew(ItemData data)
    {
        UnEquip();
        // 카메라 1인칭, 3인칭에 따라 무기 위치 변환
        if (camera.isFirstPerson)
        {   //1인칭 상태 일 때
            curEquip = Instantiate(data.equipPrefab, firstPersonEquipPosition).GetComponent<Equip>();
        }
        else
        {   //3인칭 상태 일 때
            curEquip = Instantiate(data.equipPrefab, thirdPersonEquipPosition).GetComponent<Equip>();
            curEquip.GetComponent<Animator>().StopPlayback();
        }
        curEquip.EquipEffect();
    }
    
    //장비 해제
    private void UnEquip()
    {
        if(curEquip != null)
        {
            curEquip.UnEquipEffect();
            controller.UpdateMoveSpeed();

            ThrowItem(curEquip.itemData);
            Destroy(curEquip.gameObject);
            curEquip = null;
        }
    }
    
    // 아이템 버리기 (실제론 매개변수로 들어온 데이터에 해당하는 아이템 생성)
    private void ThrowItem(ItemData data)
    {
        Instantiate(data.dropPrefab, dropPosition.position, Quaternion.Euler(Vector3.one * Random.value * 360));
    }

    // 카메라 1인칭, 3인칭에 따라 무기 위치 변환
    public void ChangePosition(bool isFirstPerson)
    {
        if (isFirstPerson)
        {   //1인칭 상태 일 때
            curEquip.transform.parent = firstPersonEquipPosition;
            curEquip.transform.localPosition = Vector3.zero;
            curEquip.transform.localRotation = Quaternion.Euler(0,0,0);
            
            //애니메이션 실행
            curEquip.GetComponent<Animator>().StopPlayback();

        }
        else
        {   //3인칭 상태 일 때
            curEquip.transform.parent = thirdPersonEquipPosition;
            curEquip.transform.localPosition = Vector3.zero;
            curEquip.transform.localRotation = Quaternion.Euler(0,0,0);
            
            //애니메이션 정지
            curEquip.GetComponent<Animator>().StartPlayback();

        }
        
    }
}
