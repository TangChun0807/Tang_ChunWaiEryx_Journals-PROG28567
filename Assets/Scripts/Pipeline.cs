using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pipeline : MonoBehaviour
{
    public float timer = 0.0f;
    public List<Vector2> pointPosition = new List<Vector2>();
    private Vector2 tempPosition = new Vector2();
    private float mag = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        Vector2 mousePosition = new Vector2(mouseWorldPosition.x, mouseWorldPosition.y);

        if (Mouse.current.leftButton.wasPressedThisFrame)
       {
            
            
                tempPosition = mousePosition;
                timer = 0.0f;







        }

        if (Mouse.current.leftButton.isPressed)
        {
            timer += Time.deltaTime;

            if (timer > 1f)
            {
                tempPosition = mousePosition;
                pointPosition.Add(tempPosition);
                timer = 0.0f;
            }


        }
       




            for (int i = 0; i < pointPosition.Count - 1; i++)
            {
                Debug.DrawLine(pointPosition[i], pointPosition[i + 1], Color.blue);
            
        }


        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {



            for (int i = 0; i < pointPosition.Count - 1; i++)
            {


                Vector2 diff = pointPosition[i] - pointPosition[i + 1];

                float x = diff.x;
                float y = diff.y;
                float xSqr = x * x;
                float ySqr = y * y;
                float sum = xSqr + ySqr;
                float magnitude = Mathf.Sqrt(sum);
                mag += magnitude;
            }

            Debug.Log(mag);
        }


    }
}



