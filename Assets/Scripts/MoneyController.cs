using System;
using UnityEngine;
using UnityEngine.Serialization;

public class MoneyController : MonoBehaviour
{
    [SerializeField] private int _moneyAtStart;
    
    private int _moneyBanq;
    public int MoneyBanq { get => _moneyBanq; }

    private void Start()
    {
        ResetComponent();
    }

    /// <summary>
    /// Add value to MoneyBanq
    /// </summary>
    /// <param name="value">Value to add int > 0</param>
    public void AddMoney(int value)
    {
        if (value < 0) return;
        _moneyBanq += value;
        UIController.Instance.UIUpdateMoney(_moneyBanq);
    }
    
    /// <summary>
    /// Sub value to MoneyBanq
    /// </summary>
    /// <param name="value">Value to substract int > 0</param>
    public void SubMoney(int value)
    {
        if (value < 0) return;
        if (_moneyBanq - value < 0) {
            _moneyBanq = 0;
        }else {
            _moneyBanq -= value;
        }
        UIController.Instance.UIUpdateMoney(_moneyBanq);
    }

    public void ResetComponent()
    {
        _moneyBanq = _moneyAtStart;
    }
    
    
}
