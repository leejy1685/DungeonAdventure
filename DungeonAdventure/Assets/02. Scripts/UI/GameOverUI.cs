using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//게임 오버 UI
public class GameOverUI : BaseUI
{
    [SerializeField] private Button reStartButton;//재시작 버튼
    [SerializeField] private Button exitButton; //게임 종료 버튼

    //생성 및 등록
    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        
        reStartButton.onClick.AddListener(OnClickReStartButton);
        exitButton.onClick.AddListener(OnClickExitButton);
    }
    
    protected override UIState GetUIState()
    {
        return UIState.GameOver;
    }

    //재시작 버튼
    void OnClickReStartButton()
    {
        GameManager.Instance.GameStart();
    }

    //종료 버튼
    void OnClickExitButton()
    {
        Application.Quit();
    }
}
