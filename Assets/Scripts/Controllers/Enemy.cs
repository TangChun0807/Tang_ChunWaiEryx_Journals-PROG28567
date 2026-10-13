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

    private void Update()
    {
        enemyChaser();
        enemyLaser();
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

}
