using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_move : MonoBehaviour
{
    Ball ball;
    public float rotateSpeed;
    private float moveX;

    void Start()
    {
        ball = FindObjectOfType<Ball>();
    }

    void Update()
    {
        if (!ball.isDied == true)
        {
            moveX = Input.GetAxis("Mouse X");

            if (Input.GetMouseButton(0))
            {
                transform.Rotate(0f, -moveX * rotateSpeed * Time.deltaTime, 0f);
            }
        }
    }

}
