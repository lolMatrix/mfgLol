using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirPatrol : Enemy
{
    [SerializeField] private Transform[] points;
    [SerializeField] private float speed;
    private int pointIteration = 0;
    private bool isWaiting = false;
    // Start is called before the first frame update
    void Start()
    {
        initrialize();
    }

    // Update is called once per frame
    void Update()
    {
        fly();
    }
    private protected bool fly()
    {
        bool forward = false;
        if (transform.position == points[pointIteration].position ) {
            pointIteration += (pointIteration == points.Length - 1) ? -pointIteration : 1;
            forward = true;
            StartCoroutine(wait());
        }

        if (!isWaiting) transform.position = Vector3.MoveTowards(transform.position, points[pointIteration].position, speed * Time.deltaTime);
        return forward;
    }
    
    private protected void initrialize()
    {
        gameObject.transform.position = points[pointIteration].position;
    }

    IEnumerator wait()
    {
        isWaiting = true;
        yield return new WaitForSeconds(3f);
        isWaiting = false;
    }
}
