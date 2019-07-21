using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;


    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Image _LivesImage;

    void Start()
    {

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
    }
}
