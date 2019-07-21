using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _gameOverText;

    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Image _LivesImage;



    void Start()
    {
        _gameOverText.gameObject.SetActive(false);
        _scoreText.text = "Score: " + 0;
    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore;
    }

    public void UpdateLives(int currentLives)
    {
      // display img Sprite
      //give it the new one

      _LivesImage.sprite = _liveSprites[currentLives];
      if (currentLives <= 0)
      {
          _gameOverText.gameObject.SetActive(true);
          StartCoroutine(GameOverFlickerRoutine());
      }

    }

    IEnumerator GameOverFlickerRoutine()
    {
        while(true)
        {
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
