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

    protected override void Awake()
    {
        base.Awake();
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
        switch (state)
        {
            case MobState.alive:
                dir = (playerPosition - transform.position).normalized;
                transform.Translate(speed * Time.deltaTime * dir);
                break;
            case MobState.dead:
                enemyAnimator.animator.SetBool("IsDead", true);
                healthBar.gameObject.SetActive(false);
                StartCoroutine(deathRoutine());
                state = MobState.inactive;
                break;
            default: 
                break;
        }
        
    }
    void FixedUpdate()
    {
        if(state == MobState.alive)
        {
            UpdateAnimationValues();
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "PlayerWeapon")
        {
            Scythe scythe = collider.GetComponent<Scythe>();
            HurtMob(scythe.scytheDamage);
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
        state = MobState.dead;
        
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
        enemyAnimator.UpdateMoveBool(true);
    }

    public override void ActivateMob()
    {
        base.ActivateMob();
        healthBar.gameObject.SetActive(true);
        health = maxHealth;
        SetHealthUI();
    }

    IEnumerator deathRoutine()
    {
        yield return new WaitForSeconds(1.26f);
        enemyAnimator.animator.SetBool("IsDead", false);
        this.gameObject.SetActive(false);
    }
}
