using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    [SerializeField]  float checkRate = 0.05f;// 상호작용 오브젝트 체크 시간
    private float lastCheckTime;// 마지막 상호작용 체크 시간
    public float maxCheckDistance;// 최대 체크 거리
    [SerializeField] LayerMask layerMask;

    public GameObject curInteractGameObject;// 현재 상호작용 게임오브젝트
    private IInteractable curInteractable;// 현재 상호작용 인터페이스
    
    private Camera camera;

    void Start()
    {
        camera = Camera.main;
    }
    
    void Update()
    {
        //라이더에서 비용이 높다고 하는데, SetActivePrompt()의 비용 낮추는 방법을 모르겠음.
        CheckInteractalbe();
    }

    private void CheckInteractalbe()
    {
        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;

            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
            {
                if (hit.collider.gameObject != curInteractGameObject)
                {
                    //아이템 정보 가져오기
                    curInteractGameObject = hit.collider.gameObject;
                    curInteractable = hit.collider.GetComponent<IInteractable>();
                    
                    //텍스트 활성화
                    curInteractable.SetActivePrompt(true);  //아이템 사용 설명
                    GameManager.Instance.uiManager.SetPromptText(true,curInteractable);    //아이템 설명
                }
            }
            else//상호작용 가능한 물건이 아닐 때
            {
                //텍스트 없애기
                curInteractable?.SetActivePrompt(false);
                GameManager.Instance.uiManager.SetPromptText(false);
                
                //아이템 정보 비우기
                curInteractGameObject = null;
                curInteractable = null;
            }
        }
    }
    

    //상호 작용 키를 눌렀을 때 (E)
    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && curInteractable != null)
        {
            //텍스트 없애기
            curInteractable.SetActivePrompt(false);
            GameManager.Instance.uiManager.SetPromptText(false);
            
            //상호작용
            curInteractable.OnInteract();
            
            //아이템 정보 비우기
            curInteractGameObject = null;
            curInteractable = null;
            
            
        }
    }
}
