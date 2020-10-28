using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeEnemy : AirPatrol
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletTime;
    [SerializeField] private Transform shootPoint;
    private bool rotate = true;

    void Start()
    {
        initrialize();
        StartCoroutine(shooting());
    }

    private IEnumerator shooting()
    {
        yield return new WaitForSeconds(bulletTime);
        Instantiate(bullet, shootPoint.transform.position, transform.rotation);
        StartCoroutine(shooting());
    }

    // Update is called once per frame
    void Update()
    {
        bool forward = fly();
        if (forward)
        {
            rotate = !rotate;
            int k = (rotate) ? 1 : 0;
            transform.rotation = Quaternion.Euler(0, 180 * k, 0);
        }
    }
}
