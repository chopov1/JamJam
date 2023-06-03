using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    private int animatorForwardBool;
    private int animatorMovingBool;

    public float velocityToStopMovingAnim;

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        animatorForwardBool = Animator.StringToHash("IsFacingForward");
        animatorMovingBool = Animator.StringToHash("IsMoving");
    }

    public void UpdateMoveBool(Vector2 velocity)
    {
        bool isMoving = true;
        if (velocity.magnitude < velocityToStopMovingAnim)
        {
            isMoving = false;
        }

        animator.SetBool(animatorMovingBool, isMoving);
    }

    public void UpdateForwardBool(bool isFacingForward)
    {
        animator.SetBool(animatorForwardBool, isFacingForward);
    }

    public void UpdateFacingDirection(bool isFacingRight)
    {
        spriteRenderer.flipX = !isFacingRight;
    }
}
