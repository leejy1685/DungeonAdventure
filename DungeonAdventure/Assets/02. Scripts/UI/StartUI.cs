using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public void OnClickStartButton()
    {
        GameManager.Instance.GameStart();
    }
    
    public void OnClickTutorialButton()
    {
        uiManager.ChangeState(UIState.Tutorial);
    }

    public void OnClickExitButton()
    {
        Application.Quit();
    }
    
    
    
    
}
