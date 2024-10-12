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
    
    private AudioSource _audioSource;

    [SerializeField] private AudioClip _wallHitSFX;
    [SerializeField] private AudioClip _paddleHitSFX;
    [SerializeField] private AudioClip _brickHitSFX;
    [SerializeField] private AudioClip _gameOverSFX;
    
    // Start is called before the first frame update
    void Start()
    {
        ResetBall();
        StartBall();
        
        if (GetComponent<AudioSource>() != null)
        {
            _audioSource = GetComponent<AudioSource>();
        }
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
                transform.position = new Vector3(
                    0.98f * xLimit * Mathf.Sign(transform.position.x), 
                    transform.position.y, 
                    transform.position.z);
                
                _audioSource.PlayOneShot(_wallHitSFX);
            }

            if (Mathf.Abs(transform.position.y) >= yLimit)
            {
                ChangeYDirection();
                _audioSource.PlayOneShot(_wallHitSFX);
            }
        }
        
        if (transform.position.y <= -yLimit)
        {
            ResetBall();
            GameBehavior.Instance.gameState = Utilities.GameState.GameOver;
            _audioSource.PlayOneShot(_gameOverSFX);
        }
    }

    private void StartBall()
    {
        _ballDirection = new Vector2(Random.value > 0.5f ? 1 : -1, 1);
    }

    private void ResetBall()
    {
        transform.position = _initialPosition;
    }

    private void ChangeXDirection()
    {
        _ballDirection.x *= -1;
    }

    private void ChangeYDirection()
    {
        _ballDirection.y *= -1;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        { 
            float xDistance = ((transform.position.x) - other.transform.position.x);
            
            float xComponent = xDistance / other.transform.lossyScale.x / _paddleAngle;
        
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
            
            _audioSource.PlayOneShot(_paddleHitSFX);
        }
        
        else if (!other.gameObject.CompareTag("Player"))
        {
            float xDistance = Mathf.Abs(other.transform.position.x - transform.position.x);
            float yDistance = Mathf.Abs(other.transform.position.y - transform.position.y);
      
            if (xDistance > yDistance)
            { 
                ChangeXDirection();
            }
            else if (xDistance < yDistance)
            {
                ChangeYDirection();
            }
            else
            {
                ChangeXDirection();
                ChangeYDirection();
            }
            
            _audioSource.PlayOneShot(_brickHitSFX);
        }
    }
}
