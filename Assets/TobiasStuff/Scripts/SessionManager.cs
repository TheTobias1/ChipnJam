using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SessionManager : MonoBehaviour
{
    public static SessionManager manager;
    public int curRound = 0;
    public List<int> combatScenes;
    public int finalLevel;
    public int musicLevel;

    public List<Abilities> playerInventory;
    public static Abilities[] PlayerInventory { get { return (SessionManager.manager != null) ? SessionManager.manager.playerInventory.ToArray() : new Abilities[0]; } }
    public bool[] earnedAbilities;

    public static bool combatScene = false;

    // Start is called before the first frame update
    void Start()
    {
        earnedAbilities = new bool[19];
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
            SessionManager.combatScene = true;
        }
        if(scene.buildIndex == finalLevel)
        {
            //End
            SessionManager.combatScene = false;
        }
    }

    public void InitiateLevel()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        SpellSpawner spellSpawner = player.GetComponentInChildren<SpellSpawner>();
        LpPlayer.acquiredLPs = new List<Abilities>();

        spellSpawner.SpawnAbilities(playerInventory.ToArray());
    }

    public void LoadCombatRound()
    {
        if (curRound < combatScenes.Count)
            SceneManager.LoadScene(combatScenes[curRound]);
        else
        { 
            SceneManager.LoadScene(0);
            Destroy(gameObject);
        }

        ++curRound;
    }

    public void LoadMusicLevel()
    {
        if(curRound != finalLevel)
            SceneManager.LoadScene(musicLevel);
        else
        {
            SceneManager.LoadScene(0);
            Destroy(gameObject);
        }
    }

    public static void LoadNext()
    {
        if(SessionManager.combatScene)
        {
            SessionManager.manager.LoadMusicLevel();
        }
        else
        {
            SessionManager.manager.LoadCombatRound();
        }
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

        playerInventory.Add(a);
    }

    public static void EarnAbility(Abilities a)
    {
        SessionManager.manager.earnedAbilities[(int)a] = true;
    }
}
