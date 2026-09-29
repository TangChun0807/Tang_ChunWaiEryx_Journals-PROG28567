using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{
    public List<Transform> starTransforms;
    public float drawingTime = 3f;

    private Vector3 currentPosition;

    Vector3 velocity;

    Vector3 directionVector;

    public float currentDistance = 0f;
    
    public  int i = 0;

    // Update is called once per frame
    void Update()
    {
        DrawConstellation();
    }

    private void DrawConstellation()
    {
        if ((currentDistance < 0.01) && (i < starTransforms.Count - 1)) { 
        
            directionVector = starTransforms[i + 1].position - starTransforms[i].position;
            velocity = directionVector.magnitude / drawingTime * directionVector.normalized;
            currentPosition = starTransforms[i].position;
            currentDistance = Vector3.Distance(currentPosition, starTransforms[i + 1].position);

            i++;
        }


        if(currentDistance >= 0.01)
        {
                currentPosition += velocity * Time.deltaTime;
                currentDistance = Vector3.Distance(currentPosition, starTransforms[i + 1].position);    
                Debug.DrawLine(starTransforms[i].position, currentPosition, Color.red);

        }
    }
       
 }

