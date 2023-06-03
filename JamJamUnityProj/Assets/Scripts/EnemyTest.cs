using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : Mob
{
    //Vector3 playerPos;
    [SerializeField] float speed;
    void Update()
    {
        Vector2 dir = (playerPosition - transform.position).normalized;
        transform.Translate(dir * speed * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerWeapon")
        {
            this.gameObject.SetActive(false);
        }
    }
}
