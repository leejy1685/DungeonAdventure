using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Start UI 클래스
public class StartUI : BaseUI
{
    [SerializeField]private Button startButton; 
    [SerializeField]private Button tutorialButton; 
    [SerializeField]private Button exitButton; 
    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        
        startButton.onClick.AddListener(OnClickStartButton);
        tutorialButton.onClick.AddListener(OnClickTutorialButton);
        exitButton.onClick.AddListener(OnClickExitButton);
        
    }

    protected override UIState GetUIState()
    {
        return UIState.Start;
    }

    //게임 시작 버튼
    public void OnClickStartButton()
    {
        GameManager.Instance.GameStart();
    }
    
    //튜토리얼 창으로 전환
    public void OnClickTutorialButton()
    {
        uiManager.ChangeState(UIState.Tutorial);
    }

    //게임 종료
    public void OnClickExitButton()
    {
        Application.Quit();
    }
    
    
    
    
}
