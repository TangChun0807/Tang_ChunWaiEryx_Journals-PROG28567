using UnityEngine;
using UnityEngine.InputSystem;


public class ConstraintsTest : MonoBehaviour
{

    private Rigidbody2D playerRb;
    public float force = 50f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            playerRb.constraints = RigidbodyConstraints2D.FreezePositionX;
        }
        if (Keyboard.current.wKey.wasPressedThisFrame)
        {
            playerRb.constraints = RigidbodyConstraints2D.FreezePositionY;
        }
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            playerRb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            playerRb.constraints = RigidbodyConstraints2D.None;
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            playerRb.AddForce(Vector2.up * force);
        }
        if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            playerRb.AddForce(Vector2.left * force);
        }
        if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            playerRb.AddForce(Vector2.right * force);
        }

    }

   
}
