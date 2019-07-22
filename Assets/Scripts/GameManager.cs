using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool _gameOver = false;

    private void Update()
    {
      if (Input.GetKeyDown(KeyCode.R) && _gameOver == true)
      {
        SceneManager.LoadScene(1); //Current Game Scene
      }
    }

    public void GameOver()
    {
      _gameOver = true;
    }
}
