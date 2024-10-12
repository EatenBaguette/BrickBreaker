using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class BrickBehavior : MonoBehaviour
{
   [SerializeField] private Player player;
   [SerializeField] private BallBehavior ball;
   [SerializeField] private GameObject halfBrick;
   [SerializeField] private GameObject quarterBrick;

   private void Start()
   {
      player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
      ball = GameObject.FindGameObjectWithTag("Ball").GetComponent<BallBehavior>();
      if (gameObject.CompareTag("Fullbrick") || gameObject.CompareTag("Halfbrick"))
      {
         halfBrick = Resources.Load("HalfBrick") as GameObject;
         quarterBrick = Resources.Load("QuarterBrick") as GameObject;
      }
   }
   
   private void OnCollisionEnter2D(Collision2D other)
   {
      if (other.gameObject.CompareTag("Ball"))
      {
         if (gameObject.CompareTag("Fullbrick"))
         {
            Vector3 position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            ScoreAndDestroy();
            Instantiate(halfBrick, position, Quaternion.identity);
         }

         if (gameObject.CompareTag("Halfbrick"))
         {
            Vector3 position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            ScoreAndDestroy();
            Instantiate(quarterBrick, position, Quaternion.identity);
         }
         else
         {
            ScoreAndDestroy();
         }
      }
   }
   private void ScoreAndDestroy()
   {
      player.Score += 10;
      Destroy(gameObject);
   }
}
