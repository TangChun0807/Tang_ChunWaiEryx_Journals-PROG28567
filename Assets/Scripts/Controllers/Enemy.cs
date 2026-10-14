using UnityEngine;


public class Enemy : MonoBehaviour
{
    public Transform playerTransform;
    public float moveSpeed = 2f;
    public float detectionRange = 5f;
    public float stoppingDistance = 3f;
    public float shootingRange = 8f;
    public float shootingInterval = 2f;
    private float shootingTimer = 0f;
    private Vector3 laserPosition;
    private Vector3 laserDirection;
    private bool laserActive = false;
    public float laserSpeed = 5f;

    public float chargeTime = 3f;
    public float rotationSpeed = 180f;
    public float rushSpeed = 8f;
    public float rushDuration = 1f;
    public float barrierRadius = 1.5f;

    private float chargeTimer = 0f;
    private float rushTimer = 0f;
    private bool isRushing = false;
    private Vector3 rushDirection;

    private void Update()
    {
        enemyChaser();
        enemyLaser();
        enemySpinningRush();
    }


     void enemyChaser()
    {
        if (playerTransform == null)
        {
            return;
        }

        float distance = Vector3.Distance(
            transform.position,
            playerTransform.position
        );

        Vector3 direction = playerTransform.position - transform.position;

        if (distance > stoppingDistance)
        {
            transform.position += direction.normalized * moveSpeed * Time.deltaTime;
        }
    }



    void enemyLaser()
    {
        if (playerTransform == null)
        {
            return;
        }

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
