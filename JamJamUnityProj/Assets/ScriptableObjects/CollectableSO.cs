using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum CollectableType { soul }
[CreateAssetMenu(fileName = "CollectableSO")]
public class CollectableSO : ScriptableObject
{
    //idk if SO is the best approach for this well see
    [SerializeField]
    List<AudioClip> soulSFX;
    public AudioClip GetCollectableSFX(CollectableType t)
    {
        switch (t)
        {
            case CollectableType.soul:
                return soulSFX[Random.Range(0, soulSFX.Count)];
                //make a default silentclip or error clip maybe 
            default: return null;
        }

    }
}
