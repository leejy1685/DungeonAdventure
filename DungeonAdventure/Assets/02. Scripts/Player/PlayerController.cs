using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Animator _animator;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
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

    #region  Move

    [Header("Movement")]
    public float moveSpeed;
    private Vector2 curMovementInput; // 현재 입력 값
    public float jumpPower;
    public LayerMask groundLayerMask; // 레이어 정보

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
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= moveSpeed;
        dir.y = _rigidbody.velocity.y;
        
        _rigidbody.velocity = dir;
        
        _animator.SetBool(MOVE,curMovementInput.magnitude > 0.1f);
    }

    //캐릭터 점프 입력과 처리
    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsGrounded())
        {
            _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
            
            _animator.SetTrigger(JUMP);
        }
    }

    //무한 점프를 막기위한 바닥에 있는지 확인하는 메서드
    private bool IsGrounded()
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
                return true;
            }
        }

        return false;
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
    
    [HideInInspector]
    public bool canLook = true;
    
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
        //camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        // 마우스 움직임의 변화량(mouseDelta)중 x(좌우)값에 민감도를 곱한다.
        // 카메라가 좌우로 회전하려면 rotation의 y 값에 넣어준다. -> 실습으로 확인
        // 좌우 회전은 플레이어(transform)를 회전시켜준다.
        // Why? 회전시킨 방향을 기준으로 앞뒤좌우 움직여야하니까.
        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }
    
    #endregion
    

}
