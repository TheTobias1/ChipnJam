using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    private HealthManager player;

    public GameObject canvas;
    public Scrollbar hpBar;

    [System.Serializable]
    public struct AbilityIcons
    {
        public Abilities abilityType;
        public Texture icon;
    }

    public List<AbilityIcons> icons;
    public static Dictionary<Abilities, Texture> iconsDictionary;

    private AbilityIcons icon1;
    private AbilityIcons icon2;
    private AbilityIcons icon3;

    private void Awake()
    {
        iconsDictionary = new Dictionary<Abilities, Texture>();
        foreach (AbilityIcons iconStruct in icons)
        {
            iconsDictionary.Add(iconStruct.abilityType, iconStruct.icon);
        }
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthManager>();

        player.onKilled += DisableHUD;
    }

    private void Update()
    {
        
    }

    private void DisableHUD()
    {
        canvas.SetActive(false);
    }
}
