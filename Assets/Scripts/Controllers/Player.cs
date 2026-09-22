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

    void Start()
    {
        Debug.Log(NormalizeVector(new Vector2(3, 4)));
        Debug.Log(NormalizeVector(new Vector2(-3, 2)));
        Debug.Log(NormalizeVector(new Vector2(1.5f, -3.5f)));

        bombOffset = new Vector2(1, 1);
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
            spawnTrailBomnb(bombTrailSpacing, numberOfTrailBomb);
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

    void spawnTrailBomnb(float bombTrailSpacing, int numberOfTrailBomb)
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
        int randomCornerPosition = UnityEngine.Random.Range(0, 4);

    }
    
}

