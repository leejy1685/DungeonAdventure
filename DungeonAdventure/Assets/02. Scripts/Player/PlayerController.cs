using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private CapsuleCollider _collider;
    private Animator _animator;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _collider = GetComponent<CapsuleCollider>();
        
        moveSpeed = defSpeed;   //초기 스피드
        canLook = false; //화면 돌리기 막기
    }
    

    // 물리 연산
    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        if (canLook)
        {
            CameraLook();
        }
    }

    #region Move
    [Header("Movement")]
    public float defSpeed;      //기본 속도
    private float moveSpeed;     //현재 속도
    
    private Vector2 curMovementInput;   // 현재 입력 값
    public float jumpPower;             // 점프 파워
    [SerializeField] private LayerMask groundLayerMask; // 레이어 바닥
    public int jumpCount = 1;       //바닥에 있을 때 초기화 되는 점프 수
    private int jumpCounting = 0;   //실제로 사용가능한 점프 수

    [SerializeField] private float runnigPower; //달리기 파워
    public float runStemina;  //달리기 시 소모 스테미나
    public bool useRun = false;        //달리는 중 판단

    [SerializeField] private LayerMask ladderLayer; //사다리 레이어

    private const string MOVE = "IsMove";
    private const string JUMP = "IsJump";
    
    //캐릭터 이동 입력 처리
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }

    //캐릭터 이동
    private void Move()
    {
        Vector3 dir = Vector3.zero;
        if (IsLadder())
        {   //사다리 타기
            _rigidbody.useGravity = false;
            dir = transform.up * curMovementInput.y + transform.right * curMovementInput.x;
            dir *= moveSpeed;
        }
        else
        {
            _rigidbody.useGravity = true;
            dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
            dir *= moveSpeed;
            dir.y = _rigidbody.velocity.y;
        }
        
        _rigidbody.velocity = dir;
        
        _animator.SetBool(MOVE,curMovementInput.magnitude > 0.1f);
    }

    private bool IsLadder()
    {
        Ray[] ray =
        {
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * _collider.center.y), transform.forward),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * _collider.center.y), transform.forward),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), transform.forward),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.01f), transform.forward)
        };

        for (int i = 0; i < ray.Length; i++)
        {
            if (Physics.Raycast(ray[i], 0.3f, ladderLayer))
            {
                return true;
            }
        }
        return false;
    }
    

    //캐릭터 점프 입력과 처리
    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            IsGrounded();
            Jump();
        }
    }

    private void Jump()
    {
        if (jumpCounting > 0)
        {
            jumpCounting--;
            _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);

            _animator.SetTrigger(JUMP);
        }
    }

    //무한 점프를 막기위한 바닥에 있는지 확인하는 메서드
    private void IsGrounded()
    {
        Ray[] ray =
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down)
        };

        for (int i = 0; i < ray.Length; i++)
        {
            if (Physics.Raycast(ray[i], 0.1f, groundLayerMask))
            {
                jumpCounting = jumpCount;
            }
        }
    }


    //달리기 입력 처리
    public void OnRunInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            useRun = Running(true);
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            if(useRun)
                useRun = Running(false);
        }
        
    }

    //달리기
    public bool Running(bool isRun)
    {
        if (isRun)
        {
            moveSpeed *= runnigPower;
        }
        else
        {
            moveSpeed /= runnigPower;
        }

        return isRun;
    }

    public void UpdateMoveSpeed(float statUp = 1)
    {
        moveSpeed = defSpeed * statUp;
    }
    

    #endregion
    
    #region Look

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook; // 최소 시야각
    public float maxXLook; // 최대 시야각
    private float camCurXRot = 0;
    public float lookSensitivity;// 카메라 민감도
    private Vector2 mouseDelta;// 마우스 변화값

    [HideInInspector] public bool canLook;
    
    // 입력값 처리
    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }
    
    void CameraLook()
    {
        // 마우스 움직임의 변화량(mouseDelta)중 y(위 아래)값에 민감도를 곱한다.
        // 카메라가 위 아래로 회전하려면 rotation의 x 값에 넣어준다. -> 실습으로 확인
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        // 마우스 움직임의 변화량(mouseDelta)중 x(좌우)값에 민감도를 곱한다.
        // 카메라가 좌우로 회전하려면 rotation의 y 값에 넣어준다. -> 실습으로 확인
        // 좌우 회전은 플레이어(transform)를 회전시켜준다.
        // Why? 회전시킨 방향을 기준으로 앞뒤좌우 움직여야하니까.
        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }
    
    #endregion
    
}
