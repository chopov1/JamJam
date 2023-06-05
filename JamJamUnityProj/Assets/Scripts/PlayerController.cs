using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Player playerReference;
    public Vector2 moveDirection;
    public Vector3 aimDirection;
    private PlayerInputActions controls;

    private Rigidbody2D playerRigidbody;
    public float walkSpeed = 10f;
    [SerializeField] float rotationalSpeed;

    public GameObject WeaponPrefab;
    public GameObject Weapon;
    public GameObject aimArrow;
    [SerializeField] private Scythe scythe;
    public bool hasWeapon;

    public PlayerAnimator playerAnimator;


    private void Awake()
    {
        controls = new PlayerInputActions();
    }
    void Start()
    {
        playerReference = GetComponent<Player>();
        Weapon = Instantiate(WeaponPrefab);
        Weapon.SetActive(false);

        hasWeapon = true;
        scythe.gameObject.SetActive(false);

        playerRigidbody = GetComponent<Rigidbody2D>();

        playerAnimator = GetComponentInChildren<PlayerAnimator>();
    }

    private void OnEnable()
    {
        controls.Enable();
        controls.Player.Throw.performed += ThrowOut;
        controls.Player.Throw.canceled += Recall;
    }
    private void OnDisable()
    {
        controls.Disable();
        controls.Player.Throw.performed -= ThrowOut;
        controls.Player.Throw.canceled -= Recall;
    }

    void Update()
    {
        GetMoveAndAimInput();
    }

    private void FixedUpdate()
    {
        MovePlayer();
        UpdateAnimationValues();
    }

    private void GetMoveAndAimInput()
    {
        moveDirection = controls.Player.Move.ReadValue<Vector2>();
        Aim(controls.Player.Aim.ReadValue<Vector2>());

    }

    private void MovePlayer()
    {
        playerRigidbody.velocity = new Vector2(moveDirection.x * walkSpeed * playerReference.currentStats.GetStat(GameStats.Stat.WalkSpeed) * Time.deltaTime, moveDirection.y * walkSpeed * playerReference.currentStats.GetStat(GameStats.Stat.WalkSpeed) * Time.deltaTime);
    }

    void Aim(Vector2 aimInput)
    {
        float angle = Mathf.Atan2(aimInput.x, aimInput.y) * Mathf.Rad2Deg;
        aimDirection = new Vector3(-aimInput.x, -aimInput.y, 0f);
        aimArrow.transform.localEulerAngles = new Vector3(0f, 0f, -angle);
        //aimDirection should in theory be the rotational equivalent of the direction the right stick is held
    }

    void ThrowOut(InputAction.CallbackContext ctx)
    {
        if(hasWeapon)
        {
            hasWeapon = false;
            scythe.gameObject.SetActive(true);
            scythe.SetScytheDirection(aimDirection);
            scythe.gameObject.transform.parent = this.transform.parent;
            scythe.goingOut = true;
        }
    }
    void OnThrow() 
    {
        scythe.SetScytheDirection(aimDirection);
    }
    void Recall(InputAction.CallbackContext ctx)
    {
        scythe.transform.parent = this.transform;
        scythe.goingOut = false;
    }

    private void UpdateAnimationValues()
    {
        if (moveDirection.x != 0)
        {
            playerAnimator.UpdateFacingDirection(moveDirection.x > 0);
        }
        if (moveDirection.y != 0)
        {
            playerAnimator.UpdateForwardBool(moveDirection.y < 0);
        }
        playerAnimator.UpdateMoveBool(playerRigidbody.velocity);
    }
}