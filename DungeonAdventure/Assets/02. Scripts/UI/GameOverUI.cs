using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : BaseUI
{
    [SerializeField] private Button reStartButton;
    [SerializeField] private Button exitButton;

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

    void OnClickReStartButton()
    {
        GameManager.Instance.GameStart();
    }

    void OnClickExitButton()
    {
        Application.Quit();
    }
}
