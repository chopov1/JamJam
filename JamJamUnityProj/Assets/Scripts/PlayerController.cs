using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector2 dir;
    [SerializeField]
    float speed;

    public GameObject WeaponPrefab;
    public GameObject Weapon;
    Boomerang boomerangScript;
    bool hasWeapon;

    // Start is called before the first frame update
    void Start()
    {
        //temp code for making the boomerang. could do object pooling or something else if we want multiple in the future doesnt have to be a prefab
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
        dir.Set(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        //keeps smooth acceleration and deccelleration, but keeps moving diagonal from being faster then moving on one axis
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
                Weapon.SetActive(true);
                Weapon.transform.position = this.transform.position;
                boomerangScript.SetPlayerPos(transform.position, dir);
            }
        }
    }
}
