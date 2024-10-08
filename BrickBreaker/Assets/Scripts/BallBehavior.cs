using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BallBehavior : MonoBehaviour
{
    public float ballSpeed = 3.0f;
    public float xLimit = 4.02f;
    public float yLimit = 4.81f;

    public bool isStarted = false;
    
    private Vector2 _ballDirection;

    [SerializeField] private Vector3 _initialPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isStarted)
        {
            transform.position += new Vector3(
                ballSpeed * _ballDirection.x,
                ballSpeed * _ballDirection.y
            ) * Time.deltaTime;

            if (Mathf.Abs(transform.position.x) >= xLimit)
            {
                changeXDirection();
            }

            if (Mathf.Abs(transform.position.y) >= yLimit)
            {
                changeYDirection();
            }
            
            
        }

        if (transform.position.y <= -yLimit)
        {
            ResetBall();
        }
    }

    public void StartBall()
    {
        isStarted = true;
        _ballDirection = new Vector2(Random.value > 0.5f ? 1 : -1, 1);
    }

    private void ResetBall()
    {
        transform.position = _initialPosition;
        isStarted = false;
    }

    public void changeXDirection()
    {
        _ballDirection.x *= -1;
    }

    public void changeYDirection()
    {
        _ballDirection.y *= -1;
    }
}
