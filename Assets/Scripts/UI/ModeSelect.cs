using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ModeSelect : UIPanel
{
    [SerializeField]
    private Button easyButton;

    [SerializeField]
    private Button normalButton;

    [SerializeField]
    private Button hardButton;

    [SerializeField]
    private Button exitButton;

    public event UnityAction<Game.Difficulty> GameStart;

    public event UnityAction Closed;

    private void Start()
    {
        easyButton.onClick.AddListener(() => GameStart?.Invoke(Game.Difficulty.Easy));
        normalButton.onClick.AddListener(() => GameStart?.Invoke(Game.Difficulty.Normal));
        hardButton.onClick.AddListener(() => GameStart?.Invoke(Game.Difficulty.Hard));
        exitButton.onClick.AddListener(() =>
        {
            Hide();
            Closed?.Invoke();
        });
    }

}
