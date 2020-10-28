using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Enemy
{
    [SerializeField] private float bulletSpeed;

    // Update is called once per frame
    private void Start()
    {
        StartCoroutine(setDisable());
    }
    void Update()
    {
        transform.Translate(Vector2.down * bulletSpeed * Time.deltaTime);
    }

    private IEnumerator setDisable()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }

}
