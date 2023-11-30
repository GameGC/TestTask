using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private HealthUpdater healthUpdater;
    [SerializeField] private PointsUpdater pointsUpdater;
    
    [SerializeField] private GameStateView gameStateView;
    [SerializeField] private HealthView healthView;
    [SerializeField] private PointsView pointsView;
    
    
    private GameStateModel m_GameStateModel;
    private int m_HealthModel;
    private int m_PointsModel;

    private void Awake() => InitModel();

    private void OnEnable()
    {
        healthUpdater.OnDamage += OnDamageReceived;
        pointsUpdater.OnPointCollected += OnPointsReceived;
        gameStateView.OnPressPlay += OnPressPlay;
    }
    private void OnDisable()
    {
        healthUpdater.OnDamage -= OnDamageReceived;
        pointsUpdater.OnPointCollected -= OnPointsReceived;
        gameStateView.OnPressPlay -= OnPressPlay;
    }

    private void InitModel()
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
    
    private void OnPointsReceived()
    {
        m_PointsModel++;
        pointsView.SetModel(m_PointsModel);
    }

    private void OnDamageReceived(int damage)
    {
        m_HealthModel -= damage;
        healthView.SetModel(m_HealthModel);
    }

}
