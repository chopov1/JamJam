using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    GameObject enemyPrefab, player;

    [SerializeField]
    int numOfObjs;

    [SerializeField]
    float SpawnSpeed;

    Queue<GameObject> enemyPool;
    List<EnemyTest> enemyTestList;
    bool readyToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        enemyTestList = new List<EnemyTest>();
        enemyPool = new Queue<GameObject>();
        instantiateObjects();
        readyToSpawn = true;
    }

    void instantiateObjects()
    {
        for(int i = 0; i < numOfObjs; i++)
        {
            GameObject gameObject = Instantiate(enemyPrefab);
            enemyTestList.Add(gameObject.GetComponent<EnemyTest>());
            gameObject.SetActive(false);
            enemyPool.Enqueue(gameObject);
        }
    }

    void spawnObject()
    {
        if(!enemyPool.Peek().activeSelf) {
            GameObject enemy = enemyPool.Dequeue();
            enemy.SetActive(true);
            enemyPool.Enqueue(enemy);
            Debug.Log("SpawnedObject");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (readyToSpawn)
        {
            StartCoroutine(SpawnEnemy(SpawnSpeed));
        }
        foreach(EnemyTest test in enemyTestList)
        {
            test.SetTarget(player.transform.position);
        }
    }

    IEnumerator SpawnEnemy(float time)
    {
        readyToSpawn = false;
        spawnObject();
        yield return new WaitForSeconds(time);
        readyToSpawn = true;
        yield return null;
    }
}
