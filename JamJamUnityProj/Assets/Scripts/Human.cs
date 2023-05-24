using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    private HumanSpawner spawner;
    private Collider2D hitbox;
    private Vector3 playerPos;
    [SerializeField]
    float speed;
    void Start()
    {
        spawner = GetComponentInParent<HumanSpawner>();
        hitbox = GetComponent<Collider2D>();
    }

    private void Update()
    {
        moveAwayFromPlayer();
    }

    void moveAwayFromPlayer()
    {
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
            this.gameObject.SetActive(false);
            spawner.humanPool.Add(this.gameObject);
            Reset();
        }
    }
    void Reset()
    {
        // reseting variables upon returning to pool
    }
}