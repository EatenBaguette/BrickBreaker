using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameBehavior : MonoBehaviour
{
   public static GameBehavior Instance;

   [SerializeField] private Player player;

   public void Awake()
   {
      if (Instance != null && Instance != this)
      {
         Destroy(this);
      }
      else
      {
         Instance = this;
      }
   }
   private void Start()
   {
      player.Score = 0;
   }
}
