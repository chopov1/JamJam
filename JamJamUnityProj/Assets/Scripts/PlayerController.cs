using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Player playerReference;
    public Vector2 moveDirection;
    public Vector2 aimDirection;
    private PlayerInputActions controls;

    private Rigidbody2D playerRigidbody;
    public float walkSpeed = 10f;

    //[SerializeField] float speed;
    [SerializeField] float rotationalSpeed;

    public GameObject WeaponPrefab;
    public GameObject Weapon;
    public GameObject aimArrow;
    Boomerang boomerangScript;
    bool hasWeapon;


    private void Awake()
    {
        controls = new PlayerInputActions();
    }
    // Start is called before the first frame update
    void Start()
    {
        playerReference = GetComponent<Player>();
        //temp code for making the boomerang. could do object pooling or something else if we want multiple in the future doesnt have to be a prefab
        Weapon = Instantiate(WeaponPrefab);
        boomerangScript = Weapon.GetComponent<Boomerang>();
        Weapon.SetActive(false);
        hasWeapon = true;
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {

        controls.Enable();
        controls.Player.Throw.performed += OnThrow;
    }

    // Update is called once per frame
    void Update()
    {
        GetMoveAndAimInput();
        
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void GetMoveAndAimInput()
    {
        moveDirection = controls.Player.Move.ReadValue<Vector2>();
        aimDirection = controls.Player.Aim.ReadValue<Vector2>();
    }

    private void MovePlayer()
    {
        playerRigidbody.velocity = new Vector2(moveDirection.x * walkSpeed * Time.deltaTime, moveDirection.y * walkSpeed * Time.deltaTime);
    }

    void OnAim(InputValue inputValue)
    {
        Vector2 aimInput = inputValue.Get<Vector2>();
        float angle = Mathf.Atan2(aimInput.x, aimInput.y) * Mathf.Rad2Deg;
        aimDirection = new Vector3(0f, 0f, -angle);
        aimArrow.transform.localEulerAngles = new Vector3(0f, 0f, -angle);
        //aimDirection should in theory be the rotational equivalent of the direction the right stick is held
    }
    void OnThrow(InputAction.CallbackContext ctx)
    {
        if(hasWeapon)
        {
            Weapon.SetActive(true);
            Weapon.transform.position = this.transform.position;
            boomerangScript.SetPlayerPos(transform.position, aimDirection);
        }
        else
        {
            
        }
    }
    void movePlayer()
    {
        moveDirection.Set(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        //keeps smooth acceleration and deccelleration, but keeps moving diagonal from being faster then moving on one axis
        if(moveDirection.magnitude > 1)
        {
            moveDirection.Normalize();
        }
        transform.Translate(moveDirection * playerReference.movementSpeed * Time.deltaTime);
    }
    void throwWeapon()
    {
        if (hasWeapon)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Weapon.SetActive(true);
                Weapon.transform.position = this.transform.position;
                boomerangScript.SetPlayerPos(transform.position, moveDirection);
            }
        }
    }
}
