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
    
    

    void Start()
    {
        Debug.Log(NormalizeVector(new Vector2(3, 4)));
        Debug.Log(NormalizeVector(new Vector2(-3, 2)));
        Debug.Log(NormalizeVector(new Vector2(1.5f, -3.5f)));
        

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
    
}

