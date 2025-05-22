using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;


public class CameraController : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer; //플레이어 레이어
    private Camera brainCamera; //브레인 카메라
    
    [SerializeField] private LayerMask equipLayer; //플레이어 레이어
    [SerializeField]  Camera EquipCamera; //브레인 카메라
    
    [SerializeField] private CinemachineVirtualCamera FirstPersonCamera;    //1인칭 카메라
    [SerializeField] private CinemachineVirtualCamera ThirdPersonCamera;    //3인칭 카메라

    private float cameraDistance;   //카메라끼리 거리

    public bool isFirstPerson;

    private void Awake()
    {
        brainCamera = GetComponent<Camera>();
        
        //처음 카메라의 거리 계산
        cameraDistance = FirstPersonCamera.transform.position.z - ThirdPersonCamera.transform.position.z;
        isFirstPerson = true;
    }
    
    //tab 키 입력시 사용
    public void OnCameraChange(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            ChangeCamera();
        }
    }

    //카메라 변환 메서드
    private void ChangeCamera()
    {
        if (isFirstPerson)
        {
            //3인칭 카메라 일때 플레이어 보이기, 장비 보이기
            brainCamera.cullingMask += playerLayer;
            brainCamera.cullingMask += equipLayer;
            //다만 장비 카메라에선 장비 안보이기
            EquipCamera.cullingMask -= equipLayer;
            
            //카메라 우선순위 변경
            FirstPersonCamera.Priority = 0;
            ThirdPersonCamera.Priority = 10;

            //상호작용 거리 조정
            CharacterManager.Instance.Player.Interaction.maxCheckDistance += cameraDistance;

            //카메라 상태 변경
            isFirstPerson = false;
            
            //무기 위치 변경
            CharacterManager.Instance.Player.Equipment.ChangePosition(isFirstPerson);
        }
        else
        {
            //1인칭 카메라 일때 플레이어, 장비 안 보이게 하기, 장비가 보이면 장비 카메라랑 겹쳐서 이상하게 보임
            brainCamera.cullingMask -= playerLayer;
            brainCamera.cullingMask -= equipLayer;
            //1인칭 카메라 일때 장비 카메라에 장비 비추기
            EquipCamera.cullingMask += equipLayer;
            
            //카메라 우선순위 변경
            FirstPersonCamera.Priority = 10;
            ThirdPersonCamera.Priority = 0;

            //상호작용 거리 조정
            CharacterManager.Instance.Player.Interaction.maxCheckDistance -= cameraDistance;

            //카메라 상태 변경
            isFirstPerson = true;
            
            //무기 위치 변경
            CharacterManager.Instance.Player.Equipment.ChangePosition(isFirstPerson);
        }
    }
}
