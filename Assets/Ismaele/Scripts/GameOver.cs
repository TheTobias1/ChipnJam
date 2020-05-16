using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private HealthManager player;
    public GameObject canvas;

    public string mainMenuName = "MainMenu";

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthManager>();

        player.onKilled += GameOverScreen;
    }

    public void GameOverScreen()
    {
        canvas.SetActive(true);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(mainMenuName);
    }
}
