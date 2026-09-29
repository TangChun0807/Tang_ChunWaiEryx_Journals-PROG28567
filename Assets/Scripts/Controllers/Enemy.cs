using UnityEngine;


public class Enemy : MonoBehaviour
{
    public Transform playerTransform;
    public float moveSpeed = 2f;
    public float detectionRange = 5f;

    private void Update()
    {
        enemyChaser();
    }

    public void enemyChaser()
    {



        if (playerTransform == null)
        {
            return;
        }


        float distance = Vector3.Distance(transform.position, playerTransform.position);

        Debug.Log(distance);

        if ( distance <= detectionRange)
        {
            Vector3 direction = playerTransform.position - transform.position;

            transform.position += direction.normalized * moveSpeed * Time.deltaTime;
        }

    }
}
