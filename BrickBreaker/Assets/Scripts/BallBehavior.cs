using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class BallBehavior : MonoBehaviour
{
    public float ballSpeed = 3.0f;
    public float xLimit = 4.02f;
    public float yLimit = 4.81f;

    [SerializeField] private float _paddleAngle = 1.0f;
        
    private Vector2 _ballDirection;

    [SerializeField] private Vector3 _initialPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        ResetBall();
        StartBall();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameBehavior.Instance.gameState == Utilities.GameState.Play)
        {
            transform.position += new Vector3(
                ballSpeed * _ballDirection.x,
                ballSpeed * _ballDirection.y
            ) * Time.deltaTime;

            if (Mathf.Abs(transform.position.x) >= xLimit)
            {
                ChangeXDirection();
            }

            if (Mathf.Abs(transform.position.y) >= yLimit)
            {
                ChangeYDirection();
            }
        }
        
        if (transform.position.y <= -yLimit)
        {
            ResetBall();
            GameBehavior.Instance.gameState = Utilities.GameState.GameOver;
        }
    }

    public void StartBall()
    {
        _ballDirection = new Vector2(Random.value > 0.5f ? 1 : -1, 1);
    }

    private void ResetBall()
    {
        transform.position = _initialPosition;
    }

    public void ChangeXDirection()
    {
        _ballDirection.x *= -1;
    }

    public void ChangeYDirection()
    {
        _ballDirection.y *= -1;
    }

    public void HitPaddleDirection(float xDistance)
    {
        float xComponent = xDistance / transform.lossyScale.x / _paddleAngle;
        
        Debug.Log("X: " + xComponent);
        Debug.Log("Y: " + Mathf.Sin(Mathf.Acos(xComponent)));
        Debug.Log("Hypot: " + Mathf.Sqrt((xComponent * xComponent) + Mathf.Pow(Mathf.Sin(Mathf.Acos(xComponent)), 2)));
        
        // Make sure not to return NaN
        if (xComponent > 1.0f)
        {
            xComponent = 1.0f;
        }
        else if (xComponent < -1.0f)
        {
            xComponent = -1.0f;
        }
        
        float yComponent = Mathf.Sqrt(2.0f) * Mathf.Sin(Mathf.Acos(xComponent / Mathf.Sqrt(2.0f)));
        
        _ballDirection = new Vector2(xComponent, yComponent);
    }

    public void HitBrick()
    {
        
    }
}
