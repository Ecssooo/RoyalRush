using System;
using UnityEngine;
using UnityEngine.Serialization;

public enum GameStates
{
    StartScreen,
    Setup,
    Battle,
    End
}

public class GameManager : MonoBehaviour
{
    #region Instance

    private static GameManager _instance;
    public static GameManager Instance { get => _instance; }

    public virtual void Awake()
    {
        if (!_instance)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    [FormerlySerializedAs("_moneyController")] [SerializeField] private MoneyController moneyControllerController;
    public MoneyController MoneyController { get => moneyControllerController; set => moneyControllerController = value; }

    [SerializeField] private EnemyManager _enemyController;

    [SerializeField] private BaseController _baseController;
    public BaseController BaseController { get => _baseController; }

    private GameStates s_gameState;
    public GameStates GameState => s_gameState;


    [Header("Gestion Musique")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _startClip;
    [SerializeField] private AudioClip _setupClip;
    [SerializeField] private AudioClip _battleClip;
    [SerializeField] private AudioClip _endClip;

    private void Start()
    {
        s_gameState = GameStates.StartScreen;
        Screen.orientation = ScreenOrientation.LandscapeRight;

        PlayMusicForCurrentState();
    }

    private void Update()
    {
        PlayMusicForCurrentState();

        switch (s_gameState)
        {
            case GameStates.StartScreen:
                _baseController.BaseAlive = true;
                break;
            case GameStates.Setup:
                _enemyController.ResetWave();
                TowerController.Instance.ResetTower();
                break;
            case GameStates.Battle:
                UIController.Instance.BlockShop(true);
                break;
            case GameStates.End:
                break;
        }
    }

    public void StartState() { s_gameState = GameStates.StartScreen; }
    public void SetupState() { s_gameState = GameStates.Setup; }
    public void BattleState() { s_gameState = GameStates.Battle; }

    public void EndState()
    {
        s_gameState = GameStates.End;
        _enemyController.ResetWave();
        TowerController.Instance.ResetTower();
        TowerController.Instance.DeleteAllTower();
        if (!_baseController.BaseAlive)
        {
            UIController.Instance.DisableAllUI();
            UIController.Instance.UpdateDefeatScreen(true);
        }
        else
        {
            UIController.Instance.DisableAllUI();
            UIController.Instance.UpdateWinScreen(true);
        }
    }

    private void PlayMusicForCurrentState()
    {
        if (!_audioSource) return;

        AudioClip clipToPlay = null;
        switch (s_gameState)
        {
            case GameStates.StartScreen: clipToPlay = _startClip; break;
            case GameStates.Setup: clipToPlay = _setupClip; break;
            case GameStates.Battle: clipToPlay = _battleClip; break;
            case GameStates.End: clipToPlay = _endClip; break;
        }

        if (clipToPlay && _audioSource.clip != clipToPlay)
        {
            _audioSource.clip = clipToPlay;
            _audioSource.Play();
        }
    }
}
