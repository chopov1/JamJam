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
    void Start()
    {
        goingOut = false;
        playerReference = FindObjectOfType<Player>();
        playerController = FindObjectOfType<PlayerController>();
    }
    void Update()
    {
        transform.Rotate(Vector3.forward, scytheRotationalSpeed * Time.deltaTime, Space.Self);
    }
    void FixedUpdate()
    {
        if(goingOut)
        {
            Vector3 nextPosition = new Vector3(throwDirection.normalized.x, throwDirection.normalized.y, 0f) * scytheSpeed;
   
            if(Vector3.Distance(playerReference.transform.position, nextPosition) < scytheDistance)
            {
                transform.position += nextPosition;
            }
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, playerReference.transform.position, scytheSpeed);
            if(Vector3.Distance(playerReference.transform.position, transform.position) < 0.5f)
            {
                playerController.hasWeapon = true;
                this.transform.position = playerReference.transform.position;
                this.gameObject.SetActive(false);          
            }
        }
    }
    public void ThrowScythe(Vector3 aimDirection)
    {
        targetPosition = playerReference.transform.position + (aimDirection * scytheDistance);
        throwDirection = (playerReference.transform.position - targetPosition);
    }
    public void RecallScythe(Transform parent)
    {
        transform.parent = parent;
        goingOut = false;
    }
}

// TODO fix bug where throw direction breaks after moving
// TODO try to implement directional influence on scythe
// TODO fix weird exponential effect using lerp