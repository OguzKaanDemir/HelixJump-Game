using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_controller : MonoBehaviour
{
    public Transform ball;
    private Vector3 offset;
    public float smoothSpeed;

    void Start()
    {
        offset = transform.position - ball.position + new Vector3(0, 1.3f, 0);
    }

    void Update()
    {
        Vector3 newPos = Vector3.Lerp(transform.position, offset + ball.position, smoothSpeed);
        transform.position = newPos;
    }
}
