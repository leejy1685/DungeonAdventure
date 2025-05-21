using System;
using System.Collections;
using System.Collections.Generic;
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
        moveSpeed = defSpeed;   //초기 스피드
        runSpeed = defSpeed * runnigPower;  //초기 달리기 속도
    }

    // 물리 연산
    private void FixedUpdate()
    {
        Move();
        UseRunStemina();
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
    [SerializeField] float defSpeed;    //기본 속도
    private float runSpeed;             //달리는 속도
    private float itemSpeed;            //아이템 먹었을 때 속도
    private float moveSpeed;            //현재 속도
    
    private Vector2 curMovementInput;   // 현재 입력 값
    public float jumpPower;             // 점프 파워
    [SerializeField] LayerMask groundLayerMask; // 레이어 정보

    [SerializeField] private float runnigPower;
    [SerializeField] float runStemina;  //달리기 시 소모 스테미나
    private bool useRun = false;        //달리는 중 판단

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


    //달리기 입력 처리
    public void OnRunInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            useRun = Running(true);
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            useRun = Running(false);
        }
        
    }

    //달리기
    private bool Running(bool isRun)
    {
        if (isRun)
        {
            moveSpeed = runSpeed;
        }
        else
        {
            moveSpeed = isSpeedUpItem ? itemSpeed : defSpeed;
        }

        return isRun;
    }

    //달리기 스태미나 소모
    private void UseRunStemina()
    {
        if (useRun)
        {
            if(!CharacterManager.Instance.Player.Condition.UseStamina(runStemina * Time.deltaTime))
                useRun = Running(false);
        }
            
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
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        // 마우스 움직임의 변화량(mouseDelta)중 x(좌우)값에 민감도를 곱한다.
        // 카메라가 좌우로 회전하려면 rotation의 y 값에 넣어준다. -> 실습으로 확인
        // 좌우 회전은 플레이어(transform)를 회전시켜준다.
        // Why? 회전시킨 방향을 기준으로 앞뒤좌우 움직여야하니까.
        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }
    
    #endregion
    
    #region Item

    private bool isSpeedUpItem;
    private Coroutine SpeedUpCoroutine;

    public void MoveSpeedUp(float value,float duration)
    {
        if (SpeedUpCoroutine != null)
        {
            SpeedUpCoroutine = null;
        }
        SpeedUpCoroutine = StartCoroutine(SpeedUp( value, duration));
    }
    
    IEnumerator SpeedUp(float value,float duration)
    {
        isSpeedUpItem = true;
        itemSpeed = defSpeed * value;
        runSpeed  = itemSpeed * runnigPower;
        moveSpeed = useRun ? runSpeed : itemSpeed;
        
        yield return new WaitForSeconds(duration);

        isSpeedUpItem = false;
        itemSpeed = defSpeed;
        runSpeed  = defSpeed * runnigPower;
        moveSpeed = useRun ? runSpeed : defSpeed;
    }

    #endregion

}
