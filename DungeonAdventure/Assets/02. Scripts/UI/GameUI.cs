using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//GameUI 클래스
public class GameUI : BaseUI
{
    public TrapIndicator trapIndicator;   //레이저 함정 걸렸을 때 화면
    
    [SerializeField]private GameObject prompt;
    
    private TextMeshProUGUI promptText;     //아이템 설명
    private TextMeshProUGUI interactionText;//상호작용 효과 설명
    

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        trapIndicator = GetComponentInChildren<TrapIndicator>();
        
        promptText = prompt.GetComponentsInChildren<TextMeshProUGUI>()[0];
        interactionText = prompt.GetComponentsInChildren<TextMeshProUGUI>()[1];
        
        prompt.SetActive(false);
    }
    protected override UIState GetUIState()
    {
        return UIState.Game;
    }
    
    //UI에 아이템 정보 표시
    public void SetPromptText(bool onOff, IInteractable curInteractable = null)
    {
        prompt.SetActive(onOff);
        promptText.text = curInteractable?.GetItemPrompt();
        interactionText.text = curInteractable?.GetInteractPrompt();
    }
}
