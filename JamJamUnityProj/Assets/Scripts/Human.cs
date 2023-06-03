using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    private Player playerReference;
    private HumanSpawner spawner;
    private Collider2D hitbox;
    private Vector3 playerPos;
    [SerializeField]
    float speed;

    [SerializeField]
    GameObject soulPrefab;

    void Start()
    {
        spawner = GetComponentInParent<HumanSpawner>();
        hitbox = GetComponent<Collider2D>();
        playerReference = FindObjectOfType<Player>();
    }

    private void Awake()
    {
        
    }



    private void Update()
    {
        moveAwayFromPlayer();
    }

    void moveAwayFromPlayer()
    {
        playerPos = playerReference.transform.position;
        Vector2 dir = (transform.position - playerPos).normalized;
        transform.Translate(dir * speed * Time.deltaTime);
    }

    public void SetTarget(Vector3 pos)
    {
        playerPos = pos;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PlayerWeapon")
        {
            DropSoul();
            this.gameObject.SetActive(false);
            spawner.humanPool.Add(this.gameObject);
            Reset();
        }
    }

    void DropSoul()
    {
        //do a random number for num of soul dropped maybe, make it var so its not magic
        for (int i = 0; i < 3; i++)
        {
            //maybe we do object pooling for this? idk if its necessary though collectable is a small object.
            GameObject g = Instantiate(soulPrefab);
            g.transform.position = new Vector3(transform.position.x + Random.Range(-3, 3), transform.position.y + Random.Range(-3, 3), transform.position.z);
        }
    }

    void Reset()
    {
        // reseting variables upon returning to pool
    }
}