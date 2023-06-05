using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    
    // Start is called before the first frame update
    [SerializeField] 
    AudioClip level1Music;

    AudioSource musicAS;
    private GameManager gameManager;

    private void Awake()
    {
        musicAS = GetComponent<AudioSource>();
        gameManager = FindObjectOfType<GameManager>();
    }
    void Start()
    {
        PlayMusic();
    }

    public void PlayMusic()
    {
        //playing around with shit, eventually Ill match music to timer or vis versa so clips end as round ends and trigger when player starts round
        musicAS.volume = 0.5f;
        switch(gameManager.waveNumber)
        {
            case 1:
                musicAS.pitch = 1;
                break;
            case 2:
                musicAS.pitch = 0.75f;
                break;
            case 3:
                musicAS.pitch = 0.5f;
                break;
            case 4:
                musicAS.pitch = 0.25f;
                break;
            default:
                musicAS.pitch = 1;
                break;
        }
        //musicAS.pitch = 1 - ((gameManager.waveNumber - 1) * gameManager.waveLengthIncrease/60);
        musicAS.clip = level1Music;
        musicAS.Play();
    }

    public void ResetMusic()
    {
        musicAS.Stop();
        PlayMusic();
    }
}
