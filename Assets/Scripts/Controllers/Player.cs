
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
   
    public float accelerationTime = 1f;
    public float decelerationTime = 1f;
    public float maxSpeed = 1f;
    [Tooltip("Measured in Degrees per second")]
    public float angularSpeed = 90f;
    public Transform enemyTransform;
    public List<Transform> asteroidTransforms = new List<Transform>();
    public int laserAmmo = 5;
    public float laserRange = 15f;
    public float laserSpeed = 5f;
    public float hitDistance = 0.7f;
    private Vector3 laserPosition;
    private Vector3 laserStartPosition;
    private Vector3 laserDirection;
    private bool laserActive = false;
    private float acceleration;
    private float deceleration;
    private Vector3 velocity = Vector3.zero;
    public float laserAngle = 90f;
    public float laserRotationSpeed = 90f;
    public float rotatingLaserLength = 5f;
    public float hitAngle = 0.01f;
 




    void Start()
    {
        acceleration = maxSpeed / accelerationTime;
        deceleration = maxSpeed / decelerationTime;
        

    }

    void Update()
    {
        PlayerMovement();
        PlayerRotation();

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            shootLaser();
        }

        laserAnimation();
        rotatingLaser();

     



    }

    private void PlayerMovement()
    {
        Vector3 playerInput = Vector3.zero;
        Keyboard current = Keyboard.current;

        if (current.leftArrowKey.isPressed || current.aKey.isPressed)
        {
            playerInput += Vector3.left;
        }

        if (current.rightArrowKey.isPressed || current.dKey.isPressed)
        {
            playerInput += Vector3.right;
        }

        if (current.upArrowKey.isPressed || current.wKey.isPressed)
        {
            playerInput += Vector3.up;
        }

        if (current.downArrowKey.isPressed || current.sKey.isPressed)
        {
            playerInput += Vector3.down;
        }

        if (playerInput.sqrMagnitude > 0)
        {
            velocity += acceleration * Time.deltaTime * playerInput.normalized;

            if (velocity.sqrMagnitude > maxSpeed * maxSpeed)
            {
                velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
            }
        }
        else
        {
            float deltaVelocity = deceleration * Time.deltaTime;

            if (velocity.magnitude > deltaVelocity)
            {
                velocity -= deltaVelocity * velocity.normalized;
            }
            else
            {
                velocity = Vector3.zero;
            }
        }

        transform.position += velocity * Time.deltaTime;
    }


    private void PlayerRotation()
    {
        if (velocity.sqrMagnitude == 0)
        {
            return;
        }

        float deltaAngle = ComputeShortestAngle(transform.up, velocity);

        if (deltaAngle == 0)
        {
            return;
        }

        float rotationDirection = Mathf.Sign(deltaAngle);
        float rotationStep = angularSpeed * Time.deltaTime;

        if (rotationStep < Mathf.Abs(deltaAngle))
        {
            transform.Rotate(0, 0, rotationStep * rotationDirection);
        }
        else
        {
            transform.Rotate(0, 0, deltaAngle);
        }
    }

    private float ComputeShortestAngle(Vector3 a, Vector3 b)
    {
        float angleA = Mathf.Atan2(a.y, a.x) * Mathf.Rad2Deg;
        float angleB = Mathf.Atan2(b.y, b.x) * Mathf.Rad2Deg;

        return Mathf.DeltaAngle(angleA, angleB);
    }

    
    private void shootLaser()
    {
        if (laserAmmo > 0 && laserActive == false)
        {
            laserStartPosition = transform.position;
            laserPosition = transform.position;

            laserDirection = transform.up.normalized;

            laserActive = true;
            laserAmmo--;

            Debug.Log("Laser Ammo: " + laserAmmo);
        }
    }

    // Move Laser and Check Targets
    private void laserAnimation()
    {
        if (laserActive == false)
        {
            return;
        }

        
        laserPosition += laserDirection * laserSpeed * Time.deltaTime;

        
        Debug.DrawLine(laserPosition, laserPosition + laserDirection, Color.red);

        
        if (enemyTransform != null)
        {
            float enemyDistance = Vector3.Distance(laserPosition, enemyTransform.position);


            if (enemyDistance <= hitDistance)
            {
                Destroy(enemyTransform.gameObject);

                laserActive = false;

                Debug.Log("Enemy Destroyed!");
                return;
            }
        }

    
        for (int i = asteroidTransforms.Count - 1; i >= 0; i--)
        {
            if (asteroidTransforms[i] == null)
            {
                asteroidTransforms.RemoveAt(i);

            }
            else
            {

                float asteroidDistance = Vector3.Distance(laserPosition, asteroidTransforms[i].position);



                if (asteroidDistance <= hitDistance)
                {
                    Destroy(asteroidTransforms[i].gameObject);

                    asteroidTransforms.RemoveAt(i);

                    laserActive = false;

                    Debug.Log("Asteroid Destroyed!");
                    return;
                }
            }
        }

        float laserDistance = Vector3.Distance(laserStartPosition, laserPosition);



        if (laserDistance >= laserRange)
        {
            laserActive = false;
        }
    }



    public void rotatingLaser()
    {
        if (Keyboard.current.jKey.isPressed)
        {
            laserAngle += laserRotationSpeed * Time.deltaTime;
        }

        if (Keyboard.current.kKey.isPressed)
        {
            laserAngle -= laserRotationSpeed * Time.deltaTime;
        }

        if (Keyboard.current.jKey.isPressed || Keyboard.current.kKey.isPressed)
        {
            float radians = laserAngle * Mathf.Deg2Rad;

            Vector3 direction = new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), 0f);

            Vector3 laserEnd = transform.position + direction * rotatingLaserLength;

            Debug.DrawLine(transform.position, laserEnd, Color.yellow);

            
            if (enemyTransform != null)
            {
                Vector3 a = enemyTransform.position - transform.position;
                Vector3 b = laserEnd - transform.position;

                float deltaAngle = Mathf.Acos(Mathf.Clamp(Vector3.Dot(a, b) / (a.magnitude * b.magnitude), -1f, 1f)) * Mathf.Rad2Deg;



                if (Vector3.Distance(transform.position, enemyTransform.position) < rotatingLaserLength && Mathf.Abs(deltaAngle) < hitAngle)
                {
                    Destroy(enemyTransform.gameObject);
                    Debug.Log("Enemy Destroyed!");
                }
            }

            
            for (int i = asteroidTransforms.Count - 1; i >= 0; i--)
            {
                if (asteroidTransforms[i] == null)
                {
                    asteroidTransforms.RemoveAt(i);
                }
                else
                {
                    Vector3 astroA = asteroidTransforms[i].position - transform.position;

                    Vector3 astroB = laserEnd - transform.position;

                    float astroDeltaAngle = Mathf.Acos(Mathf.Clamp(Vector3.Dot(astroA, astroB) / (astroA.magnitude * astroB.magnitude), -1f, 1f)) * Mathf.Rad2Deg;

                    if (Vector3.Distance(transform.position, asteroidTransforms[i].position) < rotatingLaserLength && Mathf.Abs(astroDeltaAngle) < hitAngle)
                    {
                        Destroy(asteroidTransforms[i].gameObject);

                        asteroidTransforms.RemoveAt(i);

                        Debug.Log("Asteroid Destroyed!");
                    }
                }
            }
        }
    }


    






}







