using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    private Collider2D spawnArea;
    [SerializeField] GameObject mobPrefab;
    [SerializeField] public float spawnRate;
    public List<GameObject> mobPool;
    List<Mob> mobScripts;
    [SerializeField] public int poolMaxSize; 
    private bool readyToSpawn = true;
    [SerializeField] GameObject player;
    void Start()
    {
        mobScripts = new List<Mob>();
        spawnArea = GetComponent<Collider2D>();
        mobPool = new List<GameObject>();
        InstantiateMobs();
    }
    void Update()
    {
        if(readyToSpawn)
        {
            StartCoroutine(SpawnMob(spawnRate));
        }
        SetTargets();
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
            Vector2 randomSpawnPoint = new Vector2(Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x), Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y));
            var newMob = Instantiate(mobPrefab, randomSpawnPoint, Quaternion.identity, this.transform);
            mobPool.Add(newMob);
            mobScripts.Add(newMob.GetComponent<Mob>());
            newMob.SetActive(false);
        }
    }

    void Spawn()
    {
        if (mobPool.Count > 0)
        {
            var mob = mobPool[0];
            mob.SetActive(true);
            mobPool.Remove(mob);
        }
    }

    public void ResetSpawner()
    {
        foreach (GameObject e in mobPool)
        {
            e.SetActive(false);
        }
    }
    IEnumerator SpawnMob(float time)
    {
        readyToSpawn = false;
        Spawn();
        yield return new WaitForSeconds(time);
        readyToSpawn = true;
        yield return null;
    }
}
