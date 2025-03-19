using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    #region Instance

    private static TowerController _instance;
    public static TowerController Instance { get => _instance; }
    
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

    [SerializeField] private List<GameObject> _towerList = new List<GameObject>();
    public List<GameObject> TowerList { get => _towerList; }

    public void ResetTower()
    {
        foreach(var tower in _towerList)
        {
            var script = tower.GetComponentInChildren<Towers>();
            for (int j = 0; j < script.AmmoList.Count; j++)
            {
                if(script.AmmoList[j] != null) Destroy(script.AmmoList[j].transform.parent.gameObject);
            }
            script.EnemiesList.Clear();
            script.AmmoList.Clear();
        }
    }

    public void DeleteAllTower()
    {
        for (int j = 0; j < _towerList.Count; j++)
        {
            if(_towerList[j] != null) Destroy(_towerList[j]);
        }
        _towerList.Clear();
    }
}
