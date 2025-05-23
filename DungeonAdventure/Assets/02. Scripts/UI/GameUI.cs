using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUI : BaseUI
{
    private TrapIndicator trapIndicator;   //레이저 함정 걸렸을 때 화면
    
    [SerializeField]private GameObject prompt;
    private TextMeshProUGUI promptText;     //아이템 설명

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

    public void StartTrap()
    {
        trapIndicator.Flash();
    }
    
    public void SetPromptText(bool onOff, IInteractable curInteractable = null)
    {
        prompt.SetActive(onOff);
        promptText.text = curInteractable?.GetInteractPrompt();
    }
}
