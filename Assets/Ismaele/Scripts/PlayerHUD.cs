using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    private HealthManager player;

    public GameObject canvas;
    public Image hpBar;
    public Image ability1Icon;
    public Image ability2Icon;
    public Image ability3Icon;
    public Text ability1Text;
    public Text ability2Text;
    public Text ability3Text;

    [System.Serializable]
    public struct AbilityIcons
    {
        public Abilities abilityType;
        public Sprite icon;
    }

    public List<AbilityIcons> icons;
    public static Dictionary<Abilities, Sprite> iconsDictionary;

    private Abilities a1;
    private Abilities a2;
    private Abilities a3;

    private void Awake()
    {
        iconsDictionary = new Dictionary<Abilities, Sprite>();
        foreach (AbilityIcons iconStruct in icons)
        {
            iconsDictionary.Add(iconStruct.abilityType, iconStruct.icon);
        }

        InvokeRepeating("FindPlayer", 0.0f, 1.0f);
    }

    private void FindPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthManager>();
        player.onKilled += DisableHUD;
        CancelInvoke("FindPlayer");
    }

    private void Update()
    {
        if (SessionManager.PlayerInventory.Count > 0)
        {
            a1 = SessionManager.PlayerInventory[0];
            ability1Icon.sprite = iconsDictionary[a1];
            ability1Text.text = a1.ToString();
        }


        if(SessionManager.PlayerInventory.Count > 1)
        {
            a2 = SessionManager.PlayerInventory[1];
            ability2Icon.sprite = iconsDictionary[a2];
                    ability2Text.text = a2.ToString();
        }


        if (SessionManager.PlayerInventory.Count > 2)
        {
            a3 = SessionManager.PlayerInventory[2];
            ability3Icon.sprite = iconsDictionary[a3];
            ability3Text.text = a3.ToString();
        }



        if (!Weapon.abilityActive)
        {
            if (SpellSpawner.GetAbilityType(a1) == WeaponType.Ability)
            {

                ability1Icon.enabled = false;
                ability1Text.text = "USED";
            }
            if (SpellSpawner.GetAbilityType(a2) == WeaponType.Ability)
            {

                ability2Icon.enabled = false;
                ability2Text.text = "USED";
            }
            if (SpellSpawner.GetAbilityType(a3) == WeaponType.Ability)
            {

                ability3Icon.enabled = false;
                ability3Text.text = "USED";
            }
        }

        hpBar.fillAmount = player.HealthPercent;
    }

    private void DisableHUD()
    {
        canvas.SetActive(false);
    }
}
