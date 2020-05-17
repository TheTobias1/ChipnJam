using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SessionManager : MonoBehaviour
{
    public static SessionManager manager;
    public List<int> combatScenes;

    public List<Abilities> playerInventory;
    public static Abilities[] PlayerInventory { get { return (SessionManager.manager != null) ? SessionManager.manager.playerInventory.ToArray() : new Abilities[0]; } }

    // Start is called before the first frame update
    void Start()
    {
        SessionManager.manager = this;
        DontDestroyOnLoad(gameObject);
        //SceneManager.sceneLoaded += OnLevelLoaded;

        InitiateLevel();
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        if(combatScenes.Contains(scene.buildIndex))
        {
            InitiateLevel();
        }
    }

    public void InitiateLevel()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        SpellSpawner spellSpawner = player.GetComponentInChildren<SpellSpawner>();

        spellSpawner.SpawnAbilities(playerInventory.ToArray());
    }

    public void AddAbility(int a)
    {
        AddAbility((Abilities)a);
    }

    public void AddAbility(Abilities a)
    {
        for(int i = 0; i < playerInventory.Count; ++i)
        {
            if (SpellSpawner.IsSameType(a, (Abilities)playerInventory[i]))
            {
                playerInventory[i] = a;
                return;
            }
        }
    }
}
