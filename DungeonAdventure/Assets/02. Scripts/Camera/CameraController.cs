using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;


public class CameraController : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer; //플레이어 레이어
    private Camera brainCamera; //브레인 카메라
    
    [SerializeField] private CinemachineVirtualCamera FirstPersonCamera;    //1인칭 카메라
    [SerializeField] private CinemachineVirtualCamera ThirdPersonCamera;    //3인칭 카메라

    private float cameraDistance;   //카메라끼리 거리

    private void Awake()
    {
        brainCamera = GetComponent<Camera>();
        
        //처음 카메라의 거리 계산
        cameraDistance = FirstPersonCamera.transform.position.z - ThirdPersonCamera.transform.position.z;
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
        if (FirstPersonCamera.Priority > ThirdPersonCamera.Priority)
        {
            brainCamera.cullingMask += playerLayer;
            FirstPersonCamera.Priority = 0;
            ThirdPersonCamera.Priority = 10;

            CharacterManager.Instance.Player.Interaction.maxCheckDistance += cameraDistance;
        }
        else
        {
            brainCamera.cullingMask -= playerLayer;
            FirstPersonCamera.Priority = 10;
            ThirdPersonCamera.Priority = 0;

            CharacterManager.Instance.Player.Interaction.maxCheckDistance -= cameraDistance;
        }
    }
}
