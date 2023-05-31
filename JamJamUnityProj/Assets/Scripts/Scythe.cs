using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scythe : MonoBehaviour
{
    private Player playerReference;
    private PlayerController playerController;
    [SerializeField] private float scytheSpeed;
    [SerializeField] private float scytheDistance;
    [SerializeField] private float scytheRotationalSpeed;
    public bool goingOut;
    public Vector3 targetPosition;
    private Vector3 throwDirection;

    bool isMoving;
    void Start()
    {
        
    }

    private void Awake()
    {
        goingOut = false;
        playerReference = FindObjectOfType<Player>();
        playerController = FindObjectOfType<PlayerController>();
    }
    void Update()
    {
        rotateScythe();
    }

    void rotateScythe()
    {
        float speed;
        if (!isMoving)
        {
            speed = scytheRotationalSpeed/10;
        }
        else
        {
            speed = scytheRotationalSpeed;
        }
        transform.Rotate(Vector3.forward, speed * Time.deltaTime, Space.Self);
    }
    void FixedUpdate()
    {
        if(goingOut)
        {
            isMoving = true;
            SetScytheDirection(playerController.aimDirection);
            Vector3 changeInPosition = new Vector3(throwDirection.normalized.x, throwDirection.normalized.y, 0f) * scytheSpeed;
            Vector3 nextPosition = transform.position + changeInPosition;
            if (Vector3.Distance(playerReference.transform.position, nextPosition) < scytheDistance)
            {
                transform.position = nextPosition;
            }
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, playerReference.transform.position, scytheSpeed);
            if(Vector3.Distance(playerReference.transform.position, transform.position) < 0.5f)
            {
                playerController.hasWeapon = true;
                this.transform.position = playerReference.transform.position;
                //this.gameObject.SetActive(false);
                isMoving = false;
            }
        }
    }
    public void SetScytheDirection(Vector3 aimDirection)
    {
        targetPosition = playerReference.transform.position + (aimDirection * scytheDistance);
        throwDirection = (playerReference.transform.position - targetPosition);
    }
}

// TODO fix bug where throw direction breaks after moving
// TODO fix weird exponential effect using lerp