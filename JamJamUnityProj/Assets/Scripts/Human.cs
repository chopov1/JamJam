using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : Mob
{
    //private Player playerReference;
    private Collider2D hitbox;
    //private Vector3 playerPos;
    [SerializeField] float speed;
    [SerializeField] GameObject soulPrefab;
    [SerializeField] int NumberOfSoulsToDrop, runDistance;

    void Start()
    {
        hitbox = GetComponent<Collider2D>();
        //playerReference = FindObjectOfType<Player>();
    }

    private void Awake()
    {
        
    }



    private void Update()
    {
        if (playerToClose())
        {
            moveAwayFromPlayer();
        }
    }

    bool playerToClose()
    {
        if(Vector3.Distance(playerPosition, transform.position) < runDistance)
        {
            return true;
        }
        return false;
    }

    void moveAwayFromPlayer()
    {
        //playerPosition = playerReference.transform.position;
        Vector2 dir = (transform.position - playerPosition).normalized;
        transform.Translate(dir * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerWeapon")
        {
            DropSoul();
            this.gameObject.SetActive(false);
            Reset();
        }
    }

    void DropSoul()
    {
        //do a random number for num of soul dropped maybe, make it var so its not magic
        for (int i = 0; i < NumberOfSoulsToDrop; i++)
        {
            //maybe we do object pooling for this? idk if its necessary though collectable is a small object.
            GameObject g = Instantiate(soulPrefab);
            g.transform.position = new Vector3(transform.position.x + Random.Range(-3, 3), transform.position.y + Random.Range(-3, 3), -0.02f);
        }
    }

    void Reset()
    {
        // reseting variables upon returning to pool
    }
}