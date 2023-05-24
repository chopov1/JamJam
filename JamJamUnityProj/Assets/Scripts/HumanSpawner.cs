using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanSpawner : MonoBehaviour
{
    private Collider2D spawnArea;
    [SerializeField] GameObject humanPrefab;
    [SerializeField] float spawnRate;
    public List<GameObject> humanPool;
    List<Human> humanScripts;
    [SerializeField] int poolMaxSize; 
    private bool readyToSpawn = true;

    [SerializeField]
    GameObject player;
    void Start()
    {
        humanScripts = new List<Human>();
        spawnArea = GetComponent<Collider2D>();
        humanPool = new List<GameObject>();
        InstantiateHumans();
    }
    void Update()
    {
        if(readyToSpawn)
        {
            StartCoroutine(SpawnHuman(spawnRate));
        }
        SetTargets();
    }

    void SetTargets()
    {
        foreach(var human in humanScripts)
        {
            human.SetTarget(player.transform.position);
        }
    }

    void InstantiateHumans()
    {
        for(int i = 0; i < poolMaxSize; i++)
        {
            Vector2 randomSpawnPoint = new Vector2(Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x), Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y));
            var newHuman = Instantiate(humanPrefab, randomSpawnPoint, Quaternion.identity, this.transform);
            humanPool.Add(newHuman);
            humanScripts.Add(newHuman.GetComponent<Human>());
            newHuman.SetActive(false);
        }
    }

    void SpawnHuman()
    {
        var human = humanPool[0];
        human.SetActive(true);
        humanPool.Remove(human);
    }

    IEnumerator SpawnHuman(float time)
    {
        readyToSpawn = false;
        SpawnHuman();
        yield return new WaitForSeconds(time);
        readyToSpawn = true;
        yield return null;
    }
}
