using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    private HealthManager player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthManager>();

        player.onKilled += GameOverScreen;
    }

    private void GameOverScreen()
    {

    }
}
