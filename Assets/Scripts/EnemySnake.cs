using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemySnake : Enemy
{
    // Start is called before the first frame update
    private Rigidbody2D enemy;
    private RaycastHit2D platformCheker;
    private bool isMovingLeft = true;

    [SerializeField] private GameObject checker;
    [SerializeField] private float speed;

    private void Start()
    {
        enemy = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        patrol();
    }

    private void patrol()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        platformCheker = Physics2D.Raycast(checker.transform.position, Vector2.down, 1f);
        
        turnEnemy(platformCheker.collider == null);

    }

    private void turnEnemy(bool diraction)
    {
        if (diraction)
        {
            if (isMovingLeft)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            isMovingLeft = !isMovingLeft;

        }

    }

}
