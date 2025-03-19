using UnityEngine;

[System.Serializable]
public class Wave
{
    public Transform spawnPoint;
    public float spawnInterval;
    public Transform[] waveWaypoints;
    public WeightedEnemy[] enemies;
}
