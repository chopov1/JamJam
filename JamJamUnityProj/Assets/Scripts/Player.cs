using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Player : MonoBehaviour
{
    public enum PlayerState { dead,alive}
    public PlayerState State;
    #region Upgradable Skills
    public float movementSpeed;
    public float throwMaxDistance;
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
            case "Enemy":
                playerDeath();
                break;
            default: break;
        }
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
