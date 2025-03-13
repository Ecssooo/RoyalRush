using System;
using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class Towers : MonoBehaviour
{
    [Header("Tower Params")]
    [SerializeField] private GameObject _ammo;
    [SerializeField] private float _shootDelay;
    private float t_shoot;
    
    
    //Private field
    private List<Enemy> _ennemiesList = new();
    public List<Enemy> EnemiesList { get => _ennemiesList; }


    [SerializeField] private List<Ammo> _ammoList = new();
    public List<Ammo> AmmoList { get => _ammoList; }

    [Header("Audio")]
    [SerializeField] private AudioSource _attackAudioSource;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Enemy ennemy))
        {
            _ennemiesList.Add(ennemy);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Enemy ennemy))
        {
            if (other.enabled == false)
            {
                _ennemiesList.Remove(ennemy);
            }
            for (int i = 0; i < _ennemiesList.Count; i++)
            {
                if (_ennemiesList[i] == ennemy)
                {
                    _ennemiesList.RemoveAt(i);
                }
            }
        }
    }

    private void LateUpdate()
    {
        if (t_shoot >= _shootDelay)
        {
            Shoot();
            t_shoot = 0;
        }
        else
        {
            t_shoot += Time.deltaTime;
        }
    }

    void Shoot()
    {
        if (_ennemiesList.Count == 0) return;

        // -------------- AJOUT --------------
        if (_attackAudioSource)
        {
            _attackAudioSource.Play();
        }
        // -----------------------------------

        var ammoGO = Instantiate(_ammo, transform);
        Ammo ammo = ammoGO.GetComponentInChildren<Ammo>();
        ammo.EnnemyAttach = _ennemiesList[0];
    }
}
