

using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float arrivalDistance =1f;
    public float maxFloatDistance = 10f;
    public Vector3 velocity;
    public Vector3 startPosition;
    public Vector3 endPosition;
    public Vector3 randomVector3;
    public float currentDistance = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        asteroidMovement();
    }


    public void asteroidMovement()
    {



        if (currentDistance < arrivalDistance)

        {

            startPosition = transform.position;

            randomVector3 = new UnityEngine.Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);


            while (randomVector3.magnitude < maxFloatDistance)
            {
                randomVector3 *= 2;
            }

            randomVector3 = Vector3.ClampMagnitude(randomVector3, maxFloatDistance);
            endPosition = transform.position + randomVector3;
          
        }

       // Debug.DrawLine(startPosition, endPosition, Color.red);
        velocity = moveSpeed * randomVector3.normalized;
        transform.position += velocity * Time.deltaTime;
        currentDistance = Vector3.Distance(transform.position, endPosition);


    }
}
