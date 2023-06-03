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

    [SerializeField]
    AudioClip hitTest;

    AudioSource scytheSource;
    void Start()
    {
        
    }

    private void Awake()
    {
        goingOut = false;
        playerReference = FindObjectOfType<Player>();
        playerController = FindObjectOfType<PlayerController>();
        scytheSource = GetComponent<AudioSource>();
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Human")
        {
            PlayScytheHitFX();
        }
    }

    void PlayScytheHitFX()
    {
        //could maybe handle this in the sfx for what the scythe is hitting, but for now im gonna keep a general sound around lol. Could help things sound consistent without baking it into asset, allows for modularity.
        scytheSource.pitch = Random.Range(0.7f, 1.3f);
        scytheSource.PlayOneShot(hitTest);
    }
}

// TODO fix bug where throw direction breaks after moving
// TODO fix weird exponential effect using lerp