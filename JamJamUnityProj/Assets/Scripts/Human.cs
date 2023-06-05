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
    private Vector2 dir;
    private Rigidbody2D rb;
    private PlayerAnimator humanAnimator;

    void Start()
    {
        hitbox = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        humanAnimator = GetComponentInChildren<PlayerAnimator>();
        //playerReference = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (playerToClose())
        {
            moveAwayFromPlayer();
        }
    }
    void FixedUpdate()
    {
        UpdateAnimationValues();
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
        dir = (transform.position - playerPosition).normalized;
        transform.Translate(dir * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "PlayerWeapon")
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
    private void UpdateAnimationValues()
    {
        if (dir.x != 0)
        {
            humanAnimator.UpdateFacingDirection(dir.x > 0);
        }
        if (dir.y != 0)
        {
            humanAnimator.UpdateForwardBool(dir.y < 0);
        }
        humanAnimator.UpdateMoveBool(rb.velocity);
    }
}