using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private const float speed = 3f;

    [SerializeField] private Transform cameraTarget;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(cameraTarget.transform.position.x, cameraTarget.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = cameraTarget.position;
        position.z = transform.position.z;
        transform.position = Vector3.Lerp(transform.position, position, speed * Time.deltaTime);
    }
}
