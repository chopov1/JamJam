using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Player : MonoBehaviour
{
    public enum PlayerState { dead,alive}
    public PlayerState State;
    #region Upgradable Skills
    public float health;
    public float movementSpeed;
    public float throwMaxDistance;
    public float baseEnemyDamage;
    public float defenseMulitplier;
    #endregion
    public int soulsReaped;
    AudioSource playerAS;

    [SerializeField]
    CollectableSO collectableSO;

    private void Awake()
    {
        playerAS = GetComponent<AudioSource>();
    }

    void Start()
    {
        State = PlayerState.alive;
        soulsReaped = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Soul":
                collectSoul();
                break;
            default: break;
        }
    }
    void OnTriggerStay2D(Collider2D collider)
    {
        switch (collider.gameObject.tag)
        {
            case "Enemy":
                HurtPlayer(baseEnemyDamage);
                if(health <= 0)
                {
                    playerDeath();
                }
                break;
            default: break;
        }
    }

    void HurtPlayer(float damageAmount)
    {
        health -= damageAmount * defenseMulitplier;
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
