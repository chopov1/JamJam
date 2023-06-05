using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    public enum SpawnerState { active, inactive }
    public SpawnerState State;

    [SerializeField]
    GameObject mobPrefab, player;
    bool readyToSpawn;
    private Collider2D spawnArea;
    [SerializeField] public float spawnRate;
    public Queue<GameObject> mobPool;
    List<Mob> mobScripts;
    [SerializeField] public int poolMaxSize;

    //so it doesnt go behind BG
    float zoffset = -0.02f;

    private void Awake()
    {
        mobPool = new Queue<GameObject>();
        mobScripts = new List<Mob>();
        spawnArea = GetComponent<Collider2D>();
        InstantiateMobs();
    }
    void Start()
    {
        State = SpawnerState.active;
        readyToSpawn = true;
    }
    void Update()
    {
        switch (State)
        {
            case SpawnerState.active:
                if (readyToSpawn)
                {
                    StartCoroutine(SpawnMob(spawnRate));
                }
                SetTargets();
                break;
            case SpawnerState.inactive:
                break;
        }
    }

    public void DeactivateSpawner()
    {
        State = SpawnerState.inactive;
    }

    void SetTargets()
    {
        foreach(Mob m in mobScripts)
        {
            m.SetTarget(player.transform.position);
        }
    }

    void InstantiateMobs()
    {
        for(int i = 0; i < poolMaxSize; i++)
        {
            Vector2 randomSpawnPoint = getSpawnPoint();
            GameObject newMob = Instantiate(mobPrefab, randomSpawnPoint, Quaternion.identity);
            mobPool.Enqueue(newMob);
            mobScripts.Add(newMob.GetComponent<Mob>());
            newMob.SetActive(false);
        }
    }

    private Vector3 getSpawnPoint()
    {
        var randomX = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
        var randomY = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
        var spawnPoint = new Vector3(randomX,randomY,zoffset);
        var cameraSize = new Vector3(Camera.main.pixelHeight, Camera.main.pixelWidth, 0);
        var cameraBounds = new Bounds(Camera.main.gameObject.transform.position, cameraSize);
        while(cameraBounds.Contains(spawnPoint))
        {
            randomX = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
            randomY = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
            spawnPoint = new Vector3(randomX,randomY,zoffset);
        }
        return new Vector3(Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x), Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y), zoffset);
    }

    void SpawMob()
    {
        if (!mobPool.Peek().activeSelf)
        {
            GameObject mob = mobPool.Dequeue();
            mob.transform.position = getSpawnPoint();
            mob.SetActive(true);
            mob.GetComponent<Mob>().ActivateMob();
            mobPool.Enqueue(mob);
        }
    }

    public void ResetSpawner()
    {
        foreach (GameObject mob in mobPool)
        {
            mob.SetActive(false);
        }
        State = SpawnerState.active;
    }
    IEnumerator SpawnMob(float time)
    {
        readyToSpawn = false;
        SpawMob();
        yield return new WaitForSeconds(time);
        readyToSpawn = true;
        yield return null;
    }
}
