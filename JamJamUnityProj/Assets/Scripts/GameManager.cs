using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private Player playerReference;
    private PlayerController playerController;
    
    private MobSpawner humanSpawner;
    [SerializeField] private GameObject inBetweenCanvas, hspawner;
    public int totalSouls;
    [Tooltip("waveLength is counted in total seconds")]
    [SerializeField] float waveLength;
    public float waveTime;
    public int waveNumber;
    public float humanSpawnRateMultiplier;
    public float enemySpawnRateMultiplier;

    public UnityEvent BeginWave;
    public UnityEvent EndWave;

    TextMeshProUGUI endWaveText;
    void Start()
    {
        playerReference = FindObjectOfType<Player>();
        playerController = FindObjectOfType<PlayerController>();
        waveTime = waveLength;
        waveNumber = 1;
        endWaveText = inBetweenCanvas.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
    }

    private void Awake()
    {
        humanSpawner = hspawner.GetComponent<MobSpawner>();
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
        EndWave.Invoke();
        humanSpawner.ResetSpawner();
        humanSpawner.enabled = false;
        inBetweenCanvas.SetActive(true);
        switch (playerReference.State)
        {
            case Player.PlayerState.alive:
                //sets the win image color to true
                inBetweenCanvas.transform.GetChild(0).gameObject.SetActive(false);
                inBetweenCanvas.transform.GetChild(1).gameObject.SetActive(true);
                endWaveText.text = "Wave Complete, purchase upgrades with the souls harvested from humans.";
                break;
            case Player.PlayerState.dead:
                //sets the red image to true
                inBetweenCanvas.transform.GetChild(0).gameObject.SetActive(true);
                inBetweenCanvas.transform.GetChild(1).gameObject.SetActive(false);
                endWaveText.text = "Wave Failed, you have been sent back down to the depths of hell.";
                break;
        }
        //playerController.enabled = false;
    }
    public void EndRun()
    {
        totalSouls += playerReference.soulsReaped;
    }
    public void StartNextWave()
    {
        //set player alive again
        playerReference.ResetPlayer();
        BeginWave.Invoke();
        inBetweenCanvas.SetActive(false);
        //playerController.enabled = true;
        waveTime = waveLength;
        waveNumber++;
        humanSpawner.enabled = true;
        //enemySpawner.enabled = true;
        humanSpawner.spawnRate = humanSpawner.spawnRate * humanSpawnRateMultiplier;
        //enemySpawner.spawnRate = enemySpawner.spawnRate * enemySpawnRateMultiplier;
    }
}
