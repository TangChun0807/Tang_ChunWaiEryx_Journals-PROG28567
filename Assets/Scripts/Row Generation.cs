using UnityEngine;
using TMPro;
public class RowGeneration : MonoBehaviour
{
    public  TMP_InputField inputField;
    public int squareNumber = 0;
    public float squareSize = 1f;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(squareNumber >=0)
        {

            for (int i = 0; i < squareNumber; i++)
            {
                Vector2 squarePosition = new Vector2(i * squareSize, 0);

                drawSquare(squarePosition, squareSize, Color.white);
            }
        }
       
    }

    public  void generate()
    {
        string squareInput = inputField.text;

      if(squareInput == "")
        {
            Debug.Log("Please enter a number");
            return;
        }
      
        squareNumber = int.Parse(squareInput);

    if(squareNumber <= 0)
        {
            Debug.Log("Invalid input, Please try again!");

            return;

            
        }

        

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
