using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    Vector3 playerPos;
    [SerializeField]
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dir = (playerPos - transform.position).normalized;
        transform.Translate(dir * speed * Time.deltaTime);
    }

    public void SetTarget(Vector3 pos)
    {
        playerPos = pos;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerWeapon")
        {
            this.gameObject.SetActive(false);
        }
    }
}
