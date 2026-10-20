using UnityEngine;
using UnityEngine.InputSystem;


public class Enemy : MonoBehaviour
{
    public Transform playerTransform;
    public float moveSpeed = 2f;
    public float detectionRange = 5f;
    public float stoppingDistance = 3f;
    public float shootingRange = 15f;
    public float shootingInterval = 2f;
    private float shootingTimer = 0f;
    private Vector3 laserPosition;
    private Vector3 laserDirection;
    private bool laserActive = false;
    public float laserSpeed = 10f;
    public float chargeTime = 3f;
    public float rotationSpeed = 180f;
    public float rushSpeed = 8f;
    public float rushDuration = 50.0f;
    public float barrierRadius = 1.5f;
    private float chargeTimer = 0f;
    private float rushTimer = 0f;
    private bool isRushing = false;
    private Vector3 rushDirection;
    private Vector3 velocity = Vector3.zero;
    public float angularSpeed = 90f;
    private float acceleration;
    private float deceleration;
    public float accelerationTime = 1f;
    public float decelerationTime = 1f;
    public float maxSpeed = 1f;
    public bool isChasing = true;
    public float chaseTimer = 0f;
    public float chaseTime = 5f;
    public float spinRushTime = 5f;
    public float spinRushTimer = 0f;



    private void Start()
    {
        acceleration = maxSpeed / accelerationTime;
        deceleration = maxSpeed / decelerationTime;
    }

    private void Update()
    {
        if (isChasing == true)
        {
            chaseTimer += Time.deltaTime;
            if (chaseTimer < chaseTime)
            {
                enemyChaser();
                enemyLaser();
            }
            else
            {
                isChasing = false;
                spinRushTimer = 0f;
            }
        }
        else
        {
            spinRushTimer += Time.deltaTime;
            if (spinRushTimer < spinRushTime)
            {
                enemySpinningRush();
            }
            else
            {
                isChasing = true;
                chaseTimer = 0f;
            }
                
        }







    }



    void enemyChaser()
    {
        if (playerTransform == null)
        {
            return;
        }

        
        Vector3 direction = playerTransform.position - transform.position;

        
        Vector3 enemyInput = direction.normalized;

        
        velocity += acceleration * Time.deltaTime * enemyInput;

        
        if (velocity.magnitude > maxSpeed)
        {
            velocity = velocity.normalized * maxSpeed;
        }

        
        transform.position += velocity * Time.deltaTime;

        
        float distance = Vector3.Distance(transform.position, playerTransform.position);
          
       

        if (distance < 0.5f)
        {
            Destroy(playerTransform.gameObject);
        }

        enemyRotation();
    }


    private void enemyRotation()
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







    void enemyLaser()
    {
        if (playerTransform != null)
        {

            float distance = Vector3.Distance(transform.position, playerTransform.position);

            shootingTimer += Time.deltaTime;

            if (shootingTimer >= shootingInterval && distance <= shootingRange && !laserActive)
            {
                laserPosition = transform.position;
                laserDirection = (playerTransform.position - transform.position).normalized;

                laserActive = true;
                shootingTimer = 0f;
            }

            if (laserActive)
            {
                laserPosition += laserDirection * laserSpeed * Time.deltaTime;

                Debug.DrawLine(laserPosition, laserPosition + laserDirection, Color.red);

                if (Vector3.Distance(laserPosition, playerTransform.position) < 0.5f)
                {
                    Destroy(playerTransform.gameObject);
                    laserActive = false;
                }

                if (Vector3.Distance(laserPosition, transform.position) > 15f)
                {
                    laserActive = false;
                }
            }
        }
    }


    void enemySpinningRush()
    {
        if (playerTransform == null)
        {
            return;
        }


        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        if (isRushing == false)
        {

            chargeTimer += Time.deltaTime;

            if (chargeTimer >= chargeTime)
            {

                rushDirection = playerTransform.position - transform.position;
                rushDirection = rushDirection.normalized;

                isRushing = true;
                rushTimer = 0f;
            }
        }
        else
        {

            transform.position += rushDirection * rushSpeed * Time.deltaTime;

            rushTimer += Time.deltaTime;


            int circlePoints = 20;
            float radians = 0f;

            for (int i = 0; i < circlePoints; i++)
            {
                Vector3 start = new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), 0) * barrierRadius + transform.position;


                radians += 360f / circlePoints * Mathf.Deg2Rad;

                Vector3 end = new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), 0) * barrierRadius + transform.position;



                Debug.DrawLine(start, end, Color.red);
            }


            float distance = Vector3.Distance(transform.position, playerTransform.position);



            if (distance <= barrierRadius)
            {
                Destroy(playerTransform.gameObject);
                return;
            }


            if (rushTimer >= rushDuration)
            {
                isRushing = false;
                chargeTimer = 0f;
            }
        }
    }



}



