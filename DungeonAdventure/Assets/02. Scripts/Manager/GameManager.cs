using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //싱글톤 패턴
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {   //호출 시 존재하지 않으면 생성
                _instance = new GameObject("GameManager").AddComponent<GameManager>();
            }
            return _instance;
        }
    }

    public UIManager uiManager;

    private Transform startPosition;
    private CameraController cameraController;

    public bool isPlaying;
    
    
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(this.gameObject);
        }

        startPosition = GameObject.Find("StartPosition").transform;
        cameraController = GameObject.Find("CameraController").GetComponent<CameraController>();
        
        isPlaying = false;
    }

    public void GameStart()
    {
        isPlaying = true;
        Time.timeScale = 1;
        uiManager.ChangeState(UIState.Game);   //UI 변경
        CharacterManager.Instance.Player.Condition.uiCondition.SetConditon();   //시작 컨디션
        CharacterManager.Instance.Player.transform.position = startPosition.position;   //시작 포지션
        cameraController.SetCamera();   //카메라 셋팅

        Cursor.lockState = CursorLockMode.Locked;   //화면에 마우스 고정
    }

    public void GameOver()
    {
        isPlaying = false;
        Time.timeScale = 0;
        uiManager.ChangeState(UIState.GameOver);   //UI 변경
        
        Cursor.lockState = CursorLockMode.None;   //마우스 고정 해제
    }
    
}
