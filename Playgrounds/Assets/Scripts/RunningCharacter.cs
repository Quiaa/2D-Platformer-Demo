using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningCharacter : MonoBehaviour
{

    public Rigidbody2D rigidBody2D;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public LayerMask platformLayerMask;
    public float isGroundedRayLenght;
    public float jumpForce;


    private void FixedUpdate()
    {
        HandleJump();
        animator.SetBool("isRunning", true);

    }

    private void HandleJump()
    {
        if (Input.GetKey(KeyCode.Space) && IsGrounded())
        {
            rigidBody2D.velocity = Vector2.up * jumpForce;
        }
    }
    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(spriteRenderer.bounds.center, spriteRenderer.bounds.size, 0f, Vector2.down, isGroundedRayLenght, platformLayerMask);
        return raycastHit2D.collider != null;
    }
}
