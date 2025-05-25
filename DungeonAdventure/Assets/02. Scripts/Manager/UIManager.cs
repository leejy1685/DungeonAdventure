using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIState
{
    Start,
    Tutorial,
    Game,
    GameOver
}

public class UIManager : MonoBehaviour
{
    //구현된 UI
    private StartUI startUI;
    private TutorialUI tutorialUI;
    private GameUI gameUI;
    private GameOverUI gameOverUI;
    
    private UIState currentState;// 현재 UI 상태
    
    private void Awake()
    {
        //UI 컴포넌트 가져오기
        startUI = GetComponentInChildren<StartUI>(true);
        tutorialUI = GetComponentInChildren<TutorialUI>(true);
        gameUI = GetComponentInChildren<GameUI>(true);
        gameOverUI = GetComponentInChildren<GameOverUI>(true);

        //UI 셋팅하기
        startUI.Init(this);
        tutorialUI.Init(this);
        gameUI.Init(this);
        gameOverUI.Init(this);

        
        //시작 UI
        ChangeState(UIState.Start);
    }

    //UI 변경
    public void ChangeState(UIState state)
    {
        currentState = state;
        
        startUI.SetActive(currentState);
        tutorialUI.SetActive(currentState);
        gameUI.SetActive(currentState);
        gameOverUI.SetActive(currentState);
    }

    //아이템 정보 UI 셋팅
    public void SetPromptText(bool onOff, IInteractable item = null)
    {
        gameUI.SetPromptText(onOff, item);
    }

    //TrapIndicator 전달
    public TrapIndicator getTrapIndicator()
    {
        return gameUI.trapIndicator;
    }

    
}
