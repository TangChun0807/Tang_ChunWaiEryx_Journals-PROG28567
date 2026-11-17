
using UnityEngine;
using UnityEngine.InputSystem;

public class SpaceContraints : MonoBehaviour
{
    private Rigidbody2D rb;

    public float force = 20f;
    public float rotationForce = 10f;

    private bool moveForward;
    private bool turnLeft;
    private bool turnRight;
    private bool forwardLeft;
    private bool forwardRight;
    public Transform planetTransform;

    private RigidbodyConstraints2D selectedConstraints;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        selectedConstraints = RigidbodyConstraints2D.None;
    }

    void Update()
    {
        moveForward = Keyboard.current.wKey.isPressed;
        turnLeft = Keyboard.current.aKey.isPressed;
        turnRight = Keyboard.current.dKey.isPressed;
        forwardLeft = Keyboard.current.qKey.isPressed;
        forwardRight = Keyboard.current.eKey.isPressed;




    }

    void FixedUpdate()
    {
        rb.constraints = selectedConstraints;

        if (moveForward)
        {
            rb.AddRelativeForce(Vector2.up * force);
        }

        if (turnLeft)
        {
            rb.AddTorque(rotationForce);
        }

        if (turnRight)
        {
            rb.AddTorque(-rotationForce);
        }

        if (forwardLeft)
        {
            rb.AddForceAtPosition(Vector2.up * force, planetTransform.position);

        }

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Black hole")
        {
            print("Black Hole!");
            selectedConstraints = RigidbodyConstraints2D.FreezePosition;

        }


        if (collision.gameObject.name == "planet")
        {
            print("plant!");
            selectedConstraints = RigidbodyConstraints2D.FreezePosition;
            rb.AddForceAtPosition(Vector2.up * force, planetTransform.position);


        }
    }
}
