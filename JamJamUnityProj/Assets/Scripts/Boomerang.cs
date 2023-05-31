using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    Player playerReference;
    Vector3 playerPos, endPos, dir;
    [SerializeField]
    float speed, maxDistance, rotationSpeed;

    GameObject sprite;
    bool returning;

    float curDistance;
    private void Awake()
    {
        sprite = this.transform.GetChild(0).gameObject;
        playerReference = FindObjectOfType<Player>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spin();
        moveBoomerang();
    }

    public void SetPlayerPos(Vector3 playerPos, Vector3 aimDirection)
    {
        this.playerPos = playerPos;
        endPos = playerPos + (aimDirection * maxDistance);
        returning = false;
        curDistance = 0;
        sprite.SetActive(true);
    }
    public void SetEndPosition(Vector3 aimDirection)
    {
        endPos = playerReference.transform.position + (aimDirection * maxDistance);
        returning = false;
        curDistance = 0;
        sprite.SetActive(true);
    }

    void moveBoomerang()
    {
        if(!returning)
        {
            dir = (endPos - playerReference.transform.position).normalized;
            transform.Translate(dir * speed * Time.deltaTime);
            curDistance += speed * Time.deltaTime;
            Debug.Log(curDistance);
        }
        if(returning)
        {
            dir = (playerReference.transform.position - endPos).normalized;
            transform.Translate(dir * speed * Time.deltaTime);
            curDistance -= speed * Time.deltaTime;
        }
        if(curDistance > maxDistance && !returning)
        {
            returning = true;
        }
        if(curDistance < 0)
        {
            sprite.SetActive(false);
        }
    }
    void spin()
    {
        sprite.transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime, Space.Self);
    }

    
}
