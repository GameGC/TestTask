using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum GameStateModel
{
    NotStarted,
    Playing,
    Lose
}

public class GameController : MonoBehaviour
{
    [SerializeField] private GameStateView gameStateView;
    [SerializeField] private HealthView healthView;
    [SerializeField] private PointsView pointsView;
    
    
    private GameStateModel m_GameStateModel;
    private int m_HealthModel;
    private int m_PointsModel;

    private void Awake() => InitModel();

    private void OnEnable() => gameStateView.OnPressPlay += OnPressPlay;
    private void OnDisable() => gameStateView.OnPressPlay -= OnPressPlay;

    public void InitModel()
    {
        m_GameStateModel = GameStateModel.NotStarted;
        m_HealthModel = 100;
        m_PointsModel = 0;
        
        gameStateView.SetModel(m_GameStateModel);
        healthView.SetModel(m_HealthModel);
        pointsView.SetModel(m_PointsModel);
    }
    
    private void OnPressPlay()
    {
        m_GameStateModel = GameStateModel.Playing;
        gameStateView.SetModel(m_GameStateModel);
    }
}
