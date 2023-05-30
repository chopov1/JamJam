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
    void Start()
    {
        spawner = GetComponentInParent<HumanSpawner>();
        hitbox = GetComponent<Collider2D>();
        playerReference = FindObjectOfType<Player>();
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
            this.gameObject.SetActive(false);
            spawner.humanPool.Add(this.gameObject);
            playerReference.soulsReaped++;
            Reset();
        }
    }
    void Reset()
    {
        // reseting variables upon returning to pool
    }
}