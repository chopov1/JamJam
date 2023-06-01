using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Player playerReference;
    private PlayerController playerController;
    [SerializeField] private HumanSpawner humanSpawner;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private GameObject inBetweenCanvas;
    public int totalSouls;
    [Tooltip("waveLength is counted in total seconds")]
    [SerializeField] float waveLength;
    public float waveTime;
    public int waveNumber;
    public float humanSpawnRateMultiplier;
    public float enemySpawnRateMultiplier;
    void Start()
    {
        playerReference = FindObjectOfType<Player>();
        playerController = FindObjectOfType<PlayerController>();
        waveTime = waveLength;
        waveNumber = 1;
    }

    void Update()
    {
        if(waveTime > 0f)
            waveTime -= Time.deltaTime;
        if(waveTime <= 0f)
        {
            
            EndCurrentWave();
        }
    }
    public float Upgrade(float skillToUpgrade, float amount)
    {
        return skillToUpgrade + amount;
    }
    public void EndCurrentWave()
    {
        humanSpawner.enabled = false;
        enemySpawner.enabled = false;
        inBetweenCanvas.SetActive(true);
        //playerController.enabled = false;
    }
    public void EndRun()
    {
        totalSouls += playerReference.soulsReaped;
    }
    public void StartNextWave()
    {
        inBetweenCanvas.SetActive(false);
        //playerController.enabled = true;
        waveTime = waveLength;
        waveNumber++;
        humanSpawner.enabled = true;
        enemySpawner.enabled = true;
        humanSpawner.spawnRate = humanSpawner.spawnRate * humanSpawnRateMultiplier;
        enemySpawner.spawnRate = enemySpawner.spawnRate * enemySpawnRateMultiplier;
    }
}
