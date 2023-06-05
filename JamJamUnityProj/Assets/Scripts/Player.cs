using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameStats;

[RequireComponent(typeof(AudioSource))]
public class Player : MonoBehaviour
{
    public enum PlayerState { dead,alive,inactive}
    public PlayerState State;

    float health;

    #region Upgradable Skills
    public float maxHealth;
    private float baseMaxHealth;
    public float movementSpeed;
    public float throwMaxDistance;
    public float baseEnemyDamage;
    public float defenseMulitplier;

    public float luck = 5f;

    public GameStats baseStats;
    public GameStats currentStats;

    public List<MagicItem> magicItems;
    #endregion

    public int soulsReaped;
    AudioSource playerAS;

    [SerializeField]
    CollectableSO collectableSO;

    [SerializeField]
    Slider healthBar;

    public Scythe scythe;

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
        currentStats = ScriptableObject.CreateInstance<GameStats>();

        magicItems = new List<MagicItem>();

        //MagicItem item = MagicItem.GenerateMagicItem();

        baseMaxHealth = maxHealth;
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

        float r = UnityEngine.Random.Range(0, 100);

        if (r <= (luck * currentStats.GetStat(Stat.Luck)))
        {
            // its ur lucky day
            soulsReaped++;
        }
    }

    public void CalculateStats()
    {
        foreach (Stat stat in Enum.GetValues(typeof(Stat)))
        {
            float result = baseStats.GetStat(stat);

            foreach (MagicItem m in magicItems)
            {
                result *= m.stats.GetStat(stat);
            }
            //Debug.Log($"Set {stat} = {result}");
            currentStats.SetStat(stat, result);
        }

        maxHealth = baseMaxHealth * currentStats.GetStat(Stat.Health);
        scythe.UpdateScytheStats();
        
    }

    public void AddItem(MagicItem item)
    {
        magicItems.Add(item);
        CalculateStats();
    }

    public void AddItem(ItemDisplay itemDisplay)
    {
        if (soulsReaped >= itemDisplay.item.price)
        {
            soulsReaped -= itemDisplay.item.price;
            AddItem(itemDisplay.item);
        }
    }
}
