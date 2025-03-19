using UnityEngine;
using UnityEngine.Serialization;

public class BaseController : MonoBehaviour
{
    [Header("Base Params")]
    [SerializeField] private int _baseHP = 100;
    
    [Header("UI")]
    [SerializeField] private Transform _healthBar;
    
    //Private field
    private int _currentHP;
    private bool _baseAlive;
    public bool BaseAlive { get => _baseAlive; set => _baseAlive = value; }
    
    public void Start()
    {
        _currentHP = _baseHP;
        _baseAlive = true;
        
        //UpdateHealthBar
        float ratio = (float)_currentHP / _baseHP;
        if (_healthBar != null) _healthBar.localScale = new Vector3(Mathf.Clamp01(ratio), _healthBar.localScale.y, _healthBar.localScale.z);
    }

    public void TakeDamage(int damage)
    {
        if (_currentHP >= 0)
            _currentHP -= damage;
        if (_healthBar != null)
        {
            float ratio = Mathf.Clamp01((float)_currentHP / _baseHP);
            _healthBar.localScale = new Vector3(ratio, _healthBar.localScale.y, _healthBar.localScale.z);
        }
        if (_currentHP <= 0)
        {
            BaseAlive = false;
            GameManager.Instance.EndState();
        }
    }

    public void ResetComponent()
    {
        _currentHP = _baseHP;
        _baseAlive = true;
    }
}
