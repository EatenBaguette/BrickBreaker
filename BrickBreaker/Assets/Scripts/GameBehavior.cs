using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameBehavior : MonoBehaviour
{
   public static GameBehavior Instance;

   [SerializeField] private Player player;

   [SerializeField] private GameObject _pauseImage;
   
   [SerializeField] private TextMeshProUGUI _gameOverText;

   [SerializeField] private AudioSource _soundtrack;

   public Utilities.GameState gameState = Utilities.GameState.Play;

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
      
      _pauseImage = GameObject.Find("Pause");

      _soundtrack = GameObject.Find("Soundtrack").GetComponent<AudioSource>();
      
      _pauseImage.SetActive(false);
      
      _gameOverText.enabled = false;
      
   }

   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.P))
      {
         SwitchState();
      }

      if (gameState == Utilities.GameState.GameOver)
      {
         GameOver();
      }
   }

   private void SwitchState()
   {
      switch (gameState)
      {
         case Utilities.GameState.Pause:
            gameState = Utilities.GameState.Play;
            _pauseImage.SetActive(false);
            break;
         case Utilities.GameState.Play:
            gameState = Utilities.GameState.Pause;
            _pauseImage.SetActive(true);
            break;
      }
   }

   private void GameOver()
   {
      _gameOverText.text = "Game Over";
      _gameOverText.enabled = true;
      while (_soundtrack.volume > 0)
      {
         _soundtrack.volume -= 0.1f;
      }
   }
}
