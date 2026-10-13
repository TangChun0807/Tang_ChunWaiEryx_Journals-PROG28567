using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Transform enemyTransform;
    public GameObject bombPrefab;
    public List<Transform> asteroidTransforms;
    public Vector2 bombOffset;
    public float bombTrailSpacing;
    public int numberOfTrailBomb;
    public int distance;
    public float warpRatio;
    public float inMaxRange;
    [Space(100)]
    public float maxSpeed = 1f;
    public float accelerationTime = 1f;
    Vector3 acceleration;
    private Vector3 velocity = Vector3 .zero;
    public float decelerationTime = 1f;
    public List<GameObject> spawnObject = new List<GameObject>();
    public GameObject powerUpPrefabs;
    public int laserAmmo = 5;
    public float laserRange = 10f;
    public float yellowAngle = 90f;
    public float purpleAngle = 90f;
    public float rotationSpeed = 90f;
    public float laserLength = 5f;



    void Start()
    {
       
        
        

        bombOffset = new Vector2(1, 1);
        distance = 3;
        inMaxRange = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.bKey.wasPressedThisFrame)
        {
            SpawnBombAtOffset(bombOffset);
        }

        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            spawnBombTrail(bombTrailSpacing, numberOfTrailBomb);
        }

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            spawnBombOnRandomCorner(distance);
        }

        if (Keyboard.current.wKey.wasPressedThisFrame)
        {
            warpPlayer(enemyTransform, warpRatio);
        }

        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            detectAsteroids(inMaxRange, asteroidTransforms);
        }


       

        playerMovement();

        
         enemyRadar(5, 5);

        spawnPowerups(2,5);



        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            laserShooting();
        }

        rotateLasers();



    }

    void SpawnBombAtOffset(Vector3 inOffset)
    {
        Instantiate(bombPrefab, transform.position + inOffset, Quaternion.identity);
    }

    Vector2 NormalizeVector(Vector2 inVector)
    {
        float magnitude = inVector.magnitude;

        Vector2 outVector = new Vector2(inVector.x / magnitude, inVector.y / magnitude);

        return outVector;
    }

    void spawnBombTrail(float bombTrailSpacing, int numberOfTrailBomb)
    {

        Vector2 trailBombOffset = new Vector2();

            for (int i = 1; i <= numberOfTrailBomb; i++)
            {
                trailBombOffset =  Vector2.down * bombTrailSpacing * i;

                SpawnBombAtOffset(trailBombOffset);
            }
        
    }

    void spawnBombOnRandomCorner(float distance)
    {
        int randomInt= UnityEngine.Random.Range(0, 4);

        Vector2 randomDirection = new Vector2();

        if(randomInt == 0)
        {
            randomDirection = Vector2.up + Vector2.left;
        }
        if(randomInt == 1)
        {
            randomDirection = Vector2.up + Vector2.right;
        }
        if (randomInt == 2)
        {
            randomDirection = Vector2.down + Vector2.left;
        }
        if(randomInt == 3)
        {
            randomDirection = Vector2.down + Vector2.right;
        }

        Vector2 randomOffset = randomDirection.normalized * distance;
        
        SpawnBombAtOffset(randomOffset);
        Debug.Log(randomOffset);
    }

    void warpPlayer(Transform target, float ratio)
    {
        if(ratio > 1)
        {
            ratio = 1;
        }

        Vector3 warpPosition = Vector3.Lerp(transform.position, target.position, ratio);

        transform.position = warpPosition;
    }

    void detectAsteroids(float inMaxRange, List<Transform> inAsteroids)
    {
        for(int i = 0; i < inAsteroids.Count; i++)
        {
            float distance = Vector3.Distance(transform.position, inAsteroids[i].position);
           
            Vector3 direction = inAsteroids[i].position - transform.position;

            Vector3 endPosition = transform.position + direction.normalized * 2.5f;

            if (distance <= inMaxRange)
            {
                Debug.DrawLine(transform.position, endPosition, Color.green);
            }

        }

       
    }

    void playerMovement()
    {

        acceleration = (Vector3.zero - velocity) / decelerationTime;

        if (Keyboard.current.leftArrowKey.isPressed)
        {

            acceleration = (maxSpeed * Vector3.left - velocity) / accelerationTime;

        }

        if (Keyboard.current.rightArrowKey.isPressed)
        {
            acceleration = (maxSpeed * Vector3.right - velocity) / accelerationTime;
        }


        if (Keyboard.current.upArrowKey.isPressed)
        {
            acceleration = (maxSpeed * Vector3.up - velocity) / accelerationTime;
        }


        if (Keyboard.current.downArrowKey.isPressed)
        {
            acceleration = (maxSpeed * Vector3.down - velocity) / accelerationTime;
        }




        velocity += acceleration * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;

    }


    void enemyRadar(float radius, int circlePoints)
    {



        float distance = Vector3.Distance(transform.position, enemyTransform.position);

        Color radarColor = Color.green;

        float radians= 0f;


        if(distance < radius)
        {
           radarColor = Color.red;
        }
       

        //Debug.DrawLine(transform.position, enemyTransform.position, radarColor);

        for(int i =0; i < circlePoints; i++)
        {
            Vector3 Start = new Vector3(Mathf.Sin(radians), Mathf.Cos(radians)) * radius + transform.position;
            radians += 360f / circlePoints * Mathf.Deg2Rad;
            Vector3 End = new Vector3(Mathf.Sin(radians), Mathf.Cos(radians)) * radius + transform.position;
            Debug.DrawLine(Start, End, radarColor);





        }




    }


    void spawnPowerups(float radius, int numberOfPowerUps)
    {
      
        for (int i = 0; i < spawnObject.Count; i++)
        {
            Destroy(spawnObject[i]);
        }
        spawnObject.Clear();

        float radians = 0f;
        float step = 360f / numberOfPowerUps * Mathf.Deg2Rad;

        for (int i = 0; i < numberOfPowerUps; i++)
        {
          
            Vector3 offset = new Vector3(Mathf.Cos(radians), Mathf.Sin(radians)) * radius;

            GameObject newPowerup = Instantiate(powerUpPrefabs, transform.position + offset, Quaternion.identity);

            spawnObject.Add(newPowerup);

            radians += step;
        }
    }

    void laserShooting()
    {
        if(laserAmmo > 0)
        {
            Vector3 direction = Vector3.up;

            Vector3 endPosition = transform.position + direction * laserRange;

            Debug.DrawLine(transform.position, endPosition, Color.red, 1f);

            laserAmmo--;

            Debug.Log("Laser Ammo: " + laserAmmo);
        }
    }

    void rotateLasers()
    {
        if (Keyboard.current.jKey.isPressed)
        {
            yellowAngle += rotationSpeed * Time.deltaTime;
        }

        if (Keyboard.current.kKey.isPressed)
        {
            purpleAngle -= rotationSpeed * Time.deltaTime;
        }

        float yellowRadians = yellowAngle * Mathf.Deg2Rad;
        float purpleRadians = purpleAngle * Mathf.Deg2Rad;

        Vector3 yellowDirection = new Vector3(Mathf.Cos(yellowRadians), Mathf.Sin(yellowRadians), 0);

        Vector3 purpleDirection = new Vector3(Mathf.Cos(purpleRadians), Mathf.Sin(purpleRadians), 0);
      

        Debug.DrawLine(transform.position, transform.position + yellowDirection * laserLength, Color.yellow);

        Debug.DrawLine(transform.position, transform.position + purpleDirection * laserLength, Color.magenta);
        
    }


}

