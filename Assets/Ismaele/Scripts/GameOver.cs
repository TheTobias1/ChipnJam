using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    private HealthManager player;
    public GameObject canvas;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthManager>();

        player.onKilled += GameOverScreen;
    }

    public void GameOverScreen()
    {
        Debug.Log("GAME OVER!!!");
        canvas.SetActive(true);
    }
}
