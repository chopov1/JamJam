using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Upgradable Skills
    public float movementSpeed;
    public float throwMaxDistance;
    #endregion
    public int soulsReaped;
    
    void Start()
    {
        soulsReaped = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
