using System;
using UnityEngine;
using UnityEngine.UI;

public class GameStateView : BaseView<GameStateModel>
{
    public event Action OnPressPlay;

    public GameObject playScreen;
    public Button playButton;
    
    public GameObject playingScreen;
    
    public GameObject looseScreen;


    private void OnEnable() => playButton.onClick.AddListener(OnPlayClicked);

    private void OnDisable() => playButton.onClick.RemoveListener(OnPlayClicked);

    private void OnPlayClicked() => OnPressPlay?.Invoke();
    
    public override void SetModel(GameStateModel model)
    {
        Time.timeScale = model == GameStateModel.Playing ? 1 : 0;

        playScreen.SetActive(model == GameStateModel.NotStarted);
        playingScreen.SetActive(model == GameStateModel.Playing);
        looseScreen.SetActive(model == GameStateModel.Lose);
    }
}