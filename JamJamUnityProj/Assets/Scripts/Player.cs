using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class Player : MonoBehaviour
{
    public enum PlayerState { dead,alive,inactive}
    public PlayerState State;

    float health;

    #region Upgradable Skills
    public float maxHealth;
    public float movementSpeed;
    public float throwMaxDistance;
    public float baseEnemyDamage;
    public float defenseMulitplier;

    public GameStats baseStats;
    public GameStats runStats;
    #endregion

    public int soulsReaped;
    AudioSource playerAS;

    [SerializeField]
    CollectableSO collectableSO;

    [SerializeField]
    Slider healthBar;

    private void Awake()
    {
        playerAS = GetComponent<AudioSource>();
        healthBar.maxValue = maxHealth;
    }

    void Start()
    {
        health = maxHealth;
        SetHealth();
        State = PlayerState.alive;
        soulsReaped = 0;

        baseStats = ScriptableObject.CreateInstance<GameStats>();
        runStats = ScriptableObject.CreateInstance<GameStats>();

        //MagicItem item = MagicItem.GenerateMagicItem();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        switch (collider.gameObject.tag)
        {
            case "Soul":
                collectSoul();
                break;
            default: break;
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        switch (collider.gameObject.tag)
        {
            case "Enemy":
                if(State  == PlayerState.alive)
                {
                    HurtPlayer(baseEnemyDamage);
                    if (health <= 0)
                    {
                        playerDeath();
                    }
                }
                break;
            default: break;
        }
    }

    public void ResetPlayer()
    {
        health = maxHealth;
        SetHealth();
        State = PlayerState.alive;
    }

    void SetHealth()
    {
        healthBar.value = health;
    }
    
    void HurtPlayer(float damageAmount)
    {
        health -= damageAmount * defenseMulitplier;
        SetHealth();
    }
    void playerDeath()
    {
        State = PlayerState.dead;
        soulsReaped = 0;
    }

    void collectSoul()
    {
        playerAS.PlayOneShot(collectableSO.GetCollectableSFX(CollectableType.soul), 0.5f);
        soulsReaped++;
    }
}
