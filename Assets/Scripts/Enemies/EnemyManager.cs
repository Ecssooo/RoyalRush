using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    #region Instance

    private static EnemyManager _instance;
    public static EnemyManager Instance { get => _instance; }

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
    
    
    [SerializeField] private Wave[] waves;
    [SerializeField] private Button waveButton;
    int currentWaveIndex;
    bool isWaveInProgress;
    private List<Enemy> _ennemiesList = new List<Enemy>();

    [SerializeField] private Image _coinImage;    
    
    void Start()
    {
        if (waveButton != null)
        {
            waveButton.onClick.AddListener(StartNextWave);
        }
        if (waves.Length > 0)
        {
            CheckButton();
        }
    }

    void StartNextWave()
    {
        if (!isWaveInProgress && currentWaveIndex < waves.Length)
        {
            StartCoroutine(SpawnWave(waves[currentWaveIndex]));
            currentWaveIndex++;
        }
        CheckButton();
    }

    IEnumerator SpawnWave(Wave wave)
    {
        isWaveInProgress = true;
        int totalToSpawn = wave.enemies.Sum(e => e.quantity);

        for (int i = 0; i < totalToSpawn; i++)
        {
            if (GameManager.Instance.GameState != GameStates.Battle) break;
            WeightedEnemy chosen = GetRandomEnemy(wave.enemies);
            if (chosen != null)
            {
                chosen.quantity--;
                GameObject spawned = Instantiate(chosen.enemyPrefab, wave.spawnPoint.position, Quaternion.identity);
                Enemy e = spawned.GetComponent<Enemy>();
                if (e != null)
                {
                    _ennemiesList.Add(e);
                    e.OnDie += PlayAnim;
                    e.SetWaypoints(wave.waveWaypoints);
                }
            }
            yield return new WaitForSeconds(wave.spawnInterval);
        }

        yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Enemy").Length == 0);
        if(GameManager.Instance.GameState != GameStates.End) GameManager.Instance.SetupState();
        if (currentWaveIndex >= waves.Length)
        {
            GameManager.Instance.EndState();
        }
        isWaveInProgress = false;
    }

    WeightedEnemy GetRandomEnemy(WeightedEnemy[] enemies)
    {
        var valid = enemies.Where(x => x.quantity > 0).ToList();
        if (valid.Count == 0) return null;
        return valid[Random.Range(0, valid.Count)];
    }

    void CheckButton()
    {
        if (waveButton == null) return;
        if (currentWaveIndex >= waves.Length)
        {
            waveButton.interactable = false;
        }
        else
        {
            waveButton.interactable = true;
        }
    }

    public void ResetWave()
    {
        for (int i = 0; i < _ennemiesList.Count; i++)
        {
            if(_ennemiesList[i] != null) Destroy(_ennemiesList[i].gameObject);
        }
        // _ennemiesList.Clear();
    }

    public void PlayAnim()
    {
        _coinImage.gameObject.SetActive(true);
        _coinImage.GetComponent<Animation>().Play();
    }
}
