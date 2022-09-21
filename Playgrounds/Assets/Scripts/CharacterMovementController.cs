using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharacterMovementController : MonoBehaviour
{
    public enum MovementStates
    {
        Idle,
        Running,
        Jumping,
        Attacking,
        Sprint
    }

    public enum FacingDirection
    {
        Right,
        Left
    }

    [Header("Movement Values")]
    public float movementSpeed;
    public float defaultSpeed;
    public float sprintSpeed;
    public float jumpForce;

    [Header("Raycast Lenght and LayerMask")]
    public float sideRayLenght;
    public float isGroundedRayLenght;
    public LayerMask platformLayerMask;
    
    [Header("Movement States")]
    public MovementStates movementState;
    public FacingDirection facingDirection;

    private SpriteRenderer axeRenderer;
    private Rigidbody2D rigidBody2D;
    private SpriteRenderer spriteRenderer;
    private CharacterAnimationController animController;
    private BoxCollider2D boxCollider2D;
    private float sideRayLength;

    [Header("Components")]
    [SerializeField]
    GameObject axe;

    private void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animController = GetComponent<CharacterAnimationController>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        axeRenderer = axe.GetComponent<SpriteRenderer>();
    }
 

    private void Update()
    {
        HandleJump();
    }

    private void FixedUpdate()
    {
        HandleMovement();
        IsGrounded();
        PlayAnimationsBasedOnState();
        SetCharacterDirection();
        BoxCollider();
    }

    private void HandleMovement()
    {
        rigidBody2D.constraints = RigidbodyConstraints2D.FreezeRotation;

        if (rigidBody2D.velocity.x != 0 && Input.GetKey(KeyCode.LeftShift))
        {
            if (movementSpeed == defaultSpeed)
            {
                movementSpeed += sprintSpeed;
                movementState = MovementStates.Sprint;
            }
        }
        else
        {
            animController.StopSprintingAnim();
            movementSpeed = defaultSpeed;
        }

        if (Input.GetKey(KeyCode.A))
        {
            facingDirection = CharacterMovementController.FacingDirection.Left;
            if (IsAnyRoom()) rigidBody2D.velocity = new Vector2(-movementSpeed, rigidBody2D.velocity.y);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            facingDirection = CharacterMovementController.FacingDirection.Right;
            if (IsAnyRoom()) rigidBody2D.velocity = new Vector2(+movementSpeed, rigidBody2D.velocity.y);
        }
        else // no keys pressed
        {
            rigidBody2D.velocity = new Vector2(0, rigidBody2D.velocity.y);
            rigidBody2D.constraints = RigidbodyConstraints2D.FreezePositionX;
            rigidBody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            
        }
    }   

    private void HandleJump()
    {
        if (Input.GetKey(KeyCode.Space) && IsGrounded())
        {
            rigidBody2D.velocity = Vector2.up * jumpForce;
        }
    }

    public bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size - new Vector3(0.3f, 0.2f, 0f), 0f, Vector2.down, isGroundedRayLenght, platformLayerMask);
        return raycastHit2D.collider != null;
    }

    public bool IsAnyRoom()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size - new Vector3(0.1f, 0.3f, 0f), 0f, Equals(facingDirection, FacingDirection.Right)? Vector2.right:Vector2.left, sideRayLenght, platformLayerMask);
        return raycastHit2D.collider == null;
    }
        
    private void BoxCollider()
    {
        Vector3 d1 = new Vector3(boxCollider2D.bounds.center.x - boxCollider2D.bounds.size.x / 2, boxCollider2D.bounds.center.y - boxCollider2D.bounds.size.y / 2, 0);
        Vector3 d2 = new Vector3(boxCollider2D.bounds.center.x + boxCollider2D.bounds.size.x / 2 + sideRayLength, boxCollider2D.bounds.center.y - boxCollider2D.bounds.size.y / 2, 0);
        Vector3 d3 = new Vector3(boxCollider2D.bounds.center.x + boxCollider2D.bounds.size.x / 2 + sideRayLength, boxCollider2D.bounds.center.y + boxCollider2D.bounds.size.y / 2, 0);
        Vector3 d4 = new Vector3(boxCollider2D.bounds.center.x - boxCollider2D.bounds.size.x / 2, boxCollider2D.bounds.center.y + boxCollider2D.bounds.size.y / 2, 0);
        Debug.DrawLine(d1, d2, Color.green, 0.0f, false);
        Debug.DrawLine(d2, d3, Color.green, 0.0f, false);
        Debug.DrawLine(d3, d4, Color.green, 0.0f, false);
        Debug.DrawLine(d4, d1, Color.green, 0.0f, false);
    }

    private void SetCharacterDirection()
    {
        switch (facingDirection)
        {
            case FacingDirection.Right:
                spriteRenderer.flipX = false;
                axeRenderer.flipX = false;
                axe.transform.rotation = Quaternion.Euler(0f, 0f, 27.0f);
                axe.transform.position = transform.position - new Vector3(0.4f, 0f, 0f); ;
                break;
            case FacingDirection.Left:
                spriteRenderer.flipX = true;
                axeRenderer.flipX = true;
                axe.transform.rotation = Quaternion.Euler(0f, 0f, -27.0f);
                axe.transform.position = transform.position - new Vector3(-0.5f, 0f, 0f);
                break;
        }
    }

    private void PlayAnimationsBasedOnState()
    {
        switch (movementState)
        {
            case MovementStates.Idle:
                animController.PlayIdleAnim();
                break;
            case MovementStates.Running:
                animController.PlayRunningAnim();
                break;
            case MovementStates.Jumping:
                animController.PlayJumpingAnim();
                break;
            case MovementStates.Attacking:
                animController.TriggerAttackAnimation();
                break;
            case MovementStates.Sprint:
                animController.PlaySprintingAnim();
                break;
            default:
                break;
        }
    }
}
