using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTest : Mob
{
    //Vector3 playerPos;
    [SerializeField] float speed, maxHealth;

    float health;
    public float basePlayerDmg;

    Slider healthBar;

    private void Awake()
    {
        healthBar = GetComponentInChildren<Slider>();
        health = maxHealth;
        SetHealthUI();
    }
    void Update()
    {
        Vector2 dir = (playerPosition - transform.position).normalized;
        transform.Translate(dir * speed * Time.deltaTime);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerWeapon")
        {
            HurtMob(basePlayerDmg);
            if (health <= 0)
            {
                mobDeath();
            }
        }
    }

    void HurtMob(float damageAmount)
    {
        health -= damageAmount;
        SetHealthUI();
    }

    void mobDeath()
    {
        this.gameObject.SetActive(false);
        health = maxHealth;
        SetHealthUI();
    }

    void SetHealthUI()
    {
        healthBar.value = health;
    }
}
