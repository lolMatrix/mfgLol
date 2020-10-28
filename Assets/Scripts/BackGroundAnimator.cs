using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BackGroundAnimator : MonoBehaviour
{
    [SerializeField] private float paralaxEffectCoefficient;
    [SerializeField] private GameObject playerCamera;
    private float startPositionOnX;
    private float lenght;

    // Start is called before the first frame update
    void Start()
    {
        startPositionOnX = transform.position.x;
        lenght = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float cameraBias = playerCamera.transform.position.x * (1 - paralaxEffectCoefficient);
        float distance = playerCamera.transform.position.x * paralaxEffectCoefficient;

        transform.position = new Vector3(startPositionOnX + distance, transform.position.y, transform.position.z);

        if (cameraBias > startPositionOnX + lenght)
            startPositionOnX += lenght;
        else if (cameraBias < startPositionOnX - lenght)
            startPositionOnX -= lenght;

    }
}
