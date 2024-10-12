using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PaddleBehavior : MonoBehaviour
{
    [SerializeField] private float paddleSpeed = 5.0f;

   [SerializeField] private float xLimit = 3.33f;
    
    [SerializeField] private KeyCode leftKey;
    [SerializeField] private KeyCode rightKey;

    [SerializeField] private BallBehavior ball;

    // Update is called once per frame
    void Update()
    {
        if (GameBehavior.Instance.gameState == Utilities.GameState.Play)
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
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        { 
            float xDistance = (other.transform.position.x) - (transform.position.x);

            ball.HitPaddleDirection(xDistance);
        }
    }
}
