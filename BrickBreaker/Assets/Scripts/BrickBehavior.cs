using UnityEngine;

public class BrickBehavior : MonoBehaviour
{
   [SerializeField] private Player player;
   private void OnCollisionEnter2D(Collision2D collision)
   {
      if (collision.gameObject.CompareTag("Ball"))
      {
         player.Score += 10;
         Destroy(gameObject);
      }
   }
}
