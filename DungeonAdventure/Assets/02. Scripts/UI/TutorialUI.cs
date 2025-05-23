using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    void OnClickOkButton()
    {
        uiManager.ChangeState(UIState.Start);
    }
}
