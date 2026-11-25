
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float acceleration = 15f;
    public float deceleration = 20f;
    private Rigidbody2D rigi;
    private Vector2 playerInput;
    private float horizontalSpeed = 0f;
    private FacingDirection lastDirection = FacingDirection.left;
    public Collider2D groundCollier;



    public enum FacingDirection
    {
        left, right
    }

    void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 input = new Vector2();

        // The input from the player needs to be determined and
        // then passed in the to the MovementUpdate which should
        // manage the actual movement of the character.

        if (Keyboard.current.aKey.isPressed)
        {
            input.x = -1;
        }

        if (Keyboard.current.dKey.isPressed)
        {
            input.x = 1;
        }

        MovementUpdate(input);
        Debug.Log(IsGrounded());
    }

    private void MovementUpdate(Vector2 movementInput)
    {
        playerInput = movementInput;
    }

    void FixedUpdate()
    {
        float targetSpeed = playerInput.x * moveSpeed;

        if (playerInput.x != 0)
        {
            if (horizontalSpeed < targetSpeed)
            {
                horizontalSpeed += acceleration * Time.fixedDeltaTime;

                if (horizontalSpeed > targetSpeed)
                {
                    horizontalSpeed = targetSpeed;
                }
            }

            if (horizontalSpeed > targetSpeed)
            {
                horizontalSpeed -= acceleration * Time.fixedDeltaTime;

                if (horizontalSpeed < targetSpeed)
                {
                    horizontalSpeed = targetSpeed;
                }
            }
        }
        else
        {
            if (horizontalSpeed > 0)
            {
                horizontalSpeed -= deceleration * Time.fixedDeltaTime;

                if (horizontalSpeed < 0)
                {
                    horizontalSpeed = 0;
                }
            }

            if (horizontalSpeed < 0)
            {
                horizontalSpeed += deceleration * Time.fixedDeltaTime;

                if (horizontalSpeed > 0)
                {
                    horizontalSpeed = 0;
                }
            }
        }

        rigi.linearVelocity = new Vector2(horizontalSpeed, rigi.linearVelocity.y);
    }

    public bool IsWalking()
    {
        if (rigi.linearVelocity.x != 0)
        {
            return true;
        }

        return false;
    }

    public bool IsGrounded()
    {
        if (rigi.IsTouching(groundCollier))
        {
            return true;
        }

        return false;
    }

    public FacingDirection GetFacingDirection()
    {
        if (rigi.linearVelocity.x > 0)
        {
            lastDirection = FacingDirection.right;
        }

        if (rigi.linearVelocity.x < 0)
        {
            lastDirection = FacingDirection.left;
        }

        if (lastDirection == FacingDirection.right)
        {
            return FacingDirection.right;
        }

        return FacingDirection.left;

        
    }
}




