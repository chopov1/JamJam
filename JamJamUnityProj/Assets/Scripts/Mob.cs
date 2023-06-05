using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{

    [SerializeField] protected List<AudioClip> deathSFX;

    protected AudioSource mobAS;
    public enum MobState {inactive,dead, alive}
    public MobState state;

    
    protected Player playerReference;
    protected Vector3 playerPosition;

    protected virtual void Awake()
    {
       setupAudio();
    }

    protected void setupAudio()
    {
        mobAS = GetComponent<AudioSource>();
    }

    void Start()
    {
        playerReference = FindObjectOfType<Player>();
    }
    void Update()
    {
        playerPosition = playerReference.transform.position;
    }
    public void SetTarget(Vector3 pos)
    {
        playerPosition = pos;
    }

    public virtual void ActivateMob()
    {
        state = MobState.alive;
        
    }

    protected virtual void playDeathSFX(float volumescale)
    {
        if(deathSFX.Count > 0)
        {
            mobAS.PlayOneShot(deathSFX[Random.Range(0, deathSFX.Count)], volumescale);
        }
    }

}
