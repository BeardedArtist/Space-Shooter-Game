using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private bool _isGameOver;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            SceneManager.LoadScene(1); //Current Game Scene
        }
        else if (Input.GetKeyDown(KeyCode.Q) && _isGameOver == true)
        {
            SceneManager.LoadScene(0); //Load main menu
        }
    }

    public void GameOver()
    {
        _isGameOver = true;
    }
}
