using UnityEngine;
using TMPro;
public class Player : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI _scoreUI;
  
  private int _score;

  public int Score
  {
    get => _score;

    set
    {
      _score = value;

      _scoreUI.text = Score.ToString();
    }
  }

  private void Start()
  {
    Score = 0;
  }
}
