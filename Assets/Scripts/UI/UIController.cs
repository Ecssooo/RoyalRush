using System;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    #region Instance

    private static UIController _instance;
    public static UIController Instance { get => _instance; }
    
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
    
    
    [SerializeField] private TextMeshProUGUI _moneyTXT;
    [SerializeField] private GameObject _startScreen;
    [SerializeField] private GameObject _shop;
    [SerializeField] private GameObject _shopDisable;
    [SerializeField] private GameObject _defeatScreen;
    [SerializeField] private GameObject _winScreen;
    private void Start()
    {
        SetupAllUI();
    }

    public void UIUpdateMoney(int value)
    {
        _moneyTXT.text = value.ToString();
    }
    
    private void SetupAllUI()
    {
        UIUpdateMoney(GameManager.Instance.MoneyController.MoneyBanq);
    }

    public void UpdateStartScreen(bool state) { _startScreen.SetActive(state); }

    public void UpdateShop(bool state) { _shop.SetActive(state); }
    public void BlockShop(bool state) { _shopDisable.SetActive(state);}
    
    public void UpdateDefeatScreen(bool state){_defeatScreen.SetActive(state);}
    public void UpdateWinScreen(bool state){_winScreen.SetActive(state);}
    
    public void DisableAllUI()
    {
        UpdateStartScreen(false);
        UpdateShop(false);
        BlockShop(false);
        UpdateDefeatScreen(false);
        UpdateWinScreen(false);
    }


    private void Update()
    {
        switch (GameManager.Instance.GameState)
        {
            case GameStates.StartScreen:
                DisableAllUI();
                UpdateStartScreen(true);
                break;
            case GameStates.Setup:
                DisableAllUI();
                UIUpdateMoney(GameManager.Instance.MoneyController.MoneyBanq);
                UpdateShop(true);
                BlockShop(false);
                break;
            case GameStates.Battle:
                BlockShop(true);
                break;
            case GameStates.End:
                break;
        }
    }
}
