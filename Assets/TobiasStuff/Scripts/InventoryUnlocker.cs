﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUnlocker : MonoBehaviour
{
    public GameObject[] cards;

    private void Start()
    {
        for(int i = 0; i < cards.Length; ++i)
        {
            cards[i].SetActive(SessionManager.manager.earnedAbilities[i]);
        }
    }

    public void AddAbility(int ability)
    {
        SessionManager.manager.AddAbility((Abilities)ability);
    }

    public void LoadNextLevel()
    {
        SessionManager.manager.LoadCombatRound();
    }
}
