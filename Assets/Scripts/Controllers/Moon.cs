using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    public Transform planetTransform;
    private  float currentAngle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        orbitalMotion(2f, 1f, planetTransform);
    }

    void orbitalMotion(float radius, float speed, Transform target)
    {
        currentAngle += speed * Time.deltaTime;


        float x = Mathf.Cos(currentAngle) * radius;

        float y = Mathf.Sin(currentAngle) * radius;

        transform.position = target.position + new Vector3(x, y, 0);

    }
}
