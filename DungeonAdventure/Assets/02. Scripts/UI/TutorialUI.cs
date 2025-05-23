using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//튜토리얼 UI
public class TutorialUI : BaseUI
{
    [SerializeField] private Button okButton;
    
    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        
        okButton.onClick.AddListener(OnClickOkButton);
    }

    protected override UIState GetUIState()
    {
        return UIState.Tutorial;
    }

    //게임 시작 화면으로 전환
    void OnClickOkButton()
    {
        uiManager.ChangeState(UIState.Start);
    }
}
