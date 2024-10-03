using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PaddleBehavior : MonoBehaviour
{
    public float paddleSpeed = 5.0f;

    public float xLimit = 3.33f;
    
    public KeyCode leftKey;
    public KeyCode rightKey;

    [SerializeField] private BallBehavior ball;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(leftKey) && transform.position.x > -xLimit)
        {
            transform.position -= new Vector3(paddleSpeed * Time.deltaTime, 0, 0);
        }
        
        if (Input.GetKey(rightKey) && transform.position.x < xLimit)
        {
            transform.position += new Vector3(paddleSpeed * Time.deltaTime, 0, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ball.changeYDirection();
    }
}
