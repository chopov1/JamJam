using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    Vector2 playerPos, endPos, dir;
    [SerializeField]
    float speed, distance, rotationSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //spin();
        moveBoomerang();
    }

    public void SetPlayerPos(Vector2 playerPos, Vector2 playerDir)
    {
        this.playerPos = playerPos;
        //transform.rotation = Quaternion.LookRotation(playerDir);
        endPos = playerPos + (playerDir * distance);
    }

    void moveBoomerang()
    {
        dir = (endPos - playerPos).normalized;
        transform.Translate(dir * speed * Time.deltaTime);
    }
    void spin()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime, Space.Self);
    }

    
}
