using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private Player playerReference;
    private PlayerController playerController;
    [SerializeField] private MobSpawner humanSpawner;
    [SerializeField] private MobSpawner enemySpawner;
    [SerializeField] private GameObject inBetweenCanvas;
    public int totalSouls;
    [Tooltip("waveLength is counted in total seconds")]
    [SerializeField] float waveLength;
    public float waveTime;
    public int waveNumber;
    public float humanSpawnRateMultiplier;
    public float enemySpawnRateMultiplier;

    public UnityEvent BeginWave;
    void Start()
    {
        playerReference = FindObjectOfType<Player>();
        playerController = FindObjectOfType<PlayerController>();
        waveTime = waveLength;
        waveNumber = 1;
    }

    void Update()
    {
        if(waveTime <= 0f)
        {
            
            EndCurrentWave();
        }
        else
        {
            waveTime -= Time.deltaTime;
        }
        CheckPlayerState();
    }

    void CheckPlayerState()
    {
        switch(playerReference.State)
        {
            case Player.PlayerState.alive:
                break;
            case Player.PlayerState.dead:
                waveTime = 0;
                break;
        }
    }

    public float Upgrade(float skillToUpgrade, float amount)
    {
        return skillToUpgrade + amount;
    }
    public void EndCurrentWave()
    {
        humanSpawner.ResetSpawner();
        enemySpawner.ResetSpawner();
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
        BeginWave.Invoke();
        //set player alive again
        playerReference.State = Player.PlayerState.alive;
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
