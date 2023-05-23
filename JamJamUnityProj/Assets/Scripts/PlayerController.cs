using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector2 dir;
    [SerializeField]
    float speed, friction;

    public GameObject WeaponPrefab;
    public GameObject Weapon;
    Boomerang boomerangScript;
    bool hasWeapon;

    // Start is called before the first frame update
    void Start()
    {
        Weapon = Instantiate(WeaponPrefab);
        boomerangScript = Weapon.GetComponent<Boomerang>();
        Weapon.SetActive(false);
        hasWeapon = true;
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();
        throwWeapon();
    }

    void movePlayer()
    {
        dir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if(dir.magnitude > 1)
        {
            dir.Normalize();
        }
        transform.Translate(dir * speed * Time.deltaTime);
    }
    void throwWeapon()
    {
        if (hasWeapon)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Weapon.transform.position = this.transform.position;
                Weapon.SetActive(true);
                boomerangScript.SetPlayerPos(transform.position, dir);
            }
        }
    }
}
