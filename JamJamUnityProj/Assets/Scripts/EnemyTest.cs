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
    private PlayerAnimator enemyAnimator;
    private Rigidbody2D rb;
    private Vector2 dir;

    Slider healthBar;

    private void Awake()
    {
        healthBar = GetComponentInChildren<Slider>();
        health = maxHealth;
        SetHealthUI();
    }
    void Start()
    {
        enemyAnimator = GetComponentInChildren<PlayerAnimator>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        dir = (playerPosition - transform.position).normalized;
        transform.Translate(dir * speed * Time.deltaTime);
    }
    void FixedUpdate()
    {
        UpdateAnimationValues();
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "PlayerWeapon")
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
    private void UpdateAnimationValues()
    {
        if (dir.x != 0)
        {
            enemyAnimator.UpdateFacingDirection(dir.x > 0);
        }
        if (dir.y != 0)
        {
            enemyAnimator.UpdateForwardBool(dir.y < 0);
        }
        enemyAnimator.UpdateMoveBool(rb.velocity);
    }
}
