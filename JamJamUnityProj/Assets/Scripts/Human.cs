using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    private HumanSpawner spawner;
    private Collider2D hitbox;
    void Start()
    {
        spawner = GetComponentInParent<HumanSpawner>();
        hitbox = GetComponent<Collider2D>();
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