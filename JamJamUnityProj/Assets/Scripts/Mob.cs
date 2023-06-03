using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    protected Player playerReference;
    protected Vector3 playerPosition;
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
}
