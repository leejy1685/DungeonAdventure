using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//GameUI 클래스
public class GameUI : BaseUI
{
    private TrapIndicator trapIndicator;   //레이저 함정 걸렸을 때 화면
    
    [SerializeField]private GameObject prompt;
    private TextMeshProUGUI promptText;     //아이템 설명
    
    //Condition을 먼저 구현한 이유로 GameUI에 욺기기에는 늦었다고 판단
    //Condition이 플레이어와 연동되어 있는 이유로 기능 옮기기 힘들다고 생각

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        trapIndicator = GetComponentInChildren<TrapIndicator>();

        promptText = GetComponentInChildren<TextMeshProUGUI>();
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
        promptText.text = curInteractable?.GetInteractPrompt();
    }
}
