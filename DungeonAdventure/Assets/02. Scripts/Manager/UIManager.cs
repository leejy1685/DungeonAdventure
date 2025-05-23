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
    private StartUI startUI;
    private TutorialUI tutorialUI;
    private GameUI gameUI;
    private GameOverUI gameOverUI;
    
    private UIState currentState;// 현재 UI 상태

    private void Awake()
    {
        startUI = GetComponentInChildren<StartUI>(true);
        tutorialUI = GetComponentInChildren<TutorialUI>(true);
        gameUI = GetComponentInChildren<GameUI>(true);
        gameOverUI = GetComponentInChildren<GameOverUI>(true);

        startUI.Init(this);
        tutorialUI.Init(this);
        gameUI.Init(this);
        gameOverUI.Init(this);

        ChangeState(UIState.Start);
    }

    public void ChangeState(UIState state)
    {
        currentState = state;
        
        startUI.SetActive(currentState);
        tutorialUI.SetActive(currentState);
        gameUI.SetActive(currentState);
        gameOverUI.SetActive(currentState);
    }

    public void SetPromptText(bool onOff, IInteractable item = null)
    {
        gameUI.SetPromptText(onOff, item);
    }

    public TrapIndicator getTrapIndicator()
    {
        return gameUI.trapIndicator;
    }
    
}
