
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class SquareSpawner : MonoBehaviour
{
    float squareSize = 1f;
    Vector2 lastMousePosition = Vector2.zero;
    List<Vector2> squarePositions = new List<Vector2>();


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
            squarePositions.Add(mousePosition);

        }

        float scroll = Mouse.current.scroll.ReadValue().y;

       if(scroll > 0)
        {
            squareSize--;
        }

       if(scroll < 0)
        {
            squareSize++;
        }

       
       for(int i = 0; i < squarePositions.Count; i++)
        {
            drawSquare(squarePositions[i], squareSize, Color.white);
        }

        drawSquare(mousePosition, squareSize, new Color(1, 1, 1, 0.5f));
    }

    void drawSquare(Vector2 position, float size, Color color)
    {
       

        Vector2 halfRight = Vector2.right * size * 0.5f;
        Vector2 halfUp = Vector2.up * size * 0.5f;



        

        Vector2 positionA = position + halfRight + halfUp;
        Vector2 positionB = position + halfRight - halfUp;
        Vector2 positionC = position - halfRight - halfUp;
        Vector2 positionD = position - halfRight + halfUp;


        Debug.DrawLine(positionA, positionB, color);
        Debug.DrawLine(positionB, positionC, color);
        Debug.DrawLine(positionC, positionD, color);
        Debug.DrawLine(positionD, positionA, color);

    }
}
