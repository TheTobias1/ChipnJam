using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSpawner : MonoBehaviour
{
    public Weapon currentWeapon;
    public Weapon currentAbility;

    [System.Serializable]
    public struct AbilityStructure
    {
        public Abilities abilityName;
        public GameObject abilityPrefab;
    }

    public List<AbilityStructure> abilities;
    public static Dictionary<int, GameObject> abilityDirectory;

    // Start is called before the first frame update
    void Awake()
    {
        SpellSpawner.abilityDirectory = new Dictionary<int, GameObject>();

        foreach(AbilityStructure a in abilities)
        {
            SpellSpawner.abilityDirectory.Add((int)a.abilityName, a.abilityPrefab);
        }
    }

    private void Start()
    {
        
    }

    public void SpawnAbilities(List<Abilities> newAbilities)
    {
        Debug.Log("Spawning new abilities");
        Debug.Log(SessionManager.PlayerInventory.ToArray());
        foreach(Abilities n in SessionManager.PlayerInventory)
        {
            try
            {
                GameObject newAbility = Instantiate(abilityDirectory[(int)n], transform);
                newAbility.transform.localPosition = Vector3.zero;
                newAbility.transform.localRotation = Quaternion.identity;
            }
            catch
            {
                Debug.Log("BAD ABILITY: " + (int)n);
                continue;
            }

        }
    }

    public void SpawnAbility(Abilities n)
    {
        GameObject newAbility = Instantiate(abilityDirectory[(int)n], transform);
        newAbility.transform.localPosition = Vector3.zero;
        newAbility.transform.localRotation = Quaternion.identity;
    }

    public static bool IsSameType(Abilities a, Abilities b)
    {
        return SpellSpawner.GetAbilityType(a) == SpellSpawner.GetAbilityType(b);
    }

    public static WeaponType GetAbilityType(Abilities a)
    {
        if ((int)a <= 9)
        {
            return WeaponType.Attack;
        }
        else if((int)a <= 14)
        {
            return WeaponType.Movement;
        }
        else
        {
            return WeaponType.Ability;
        }
    }
}

public enum Abilities { AoeLauncher = 0, BasicLauncher = 1, ClusterLauncher = 2, Orbgun = 3, PeaShooter = 4, Scatter = 5, Tribomb = 6, Fireball = 7, UltimateTriBomb = 8, DualAoe = 9, BlastOff = 10, DoubleJump = 11, Slam = 12, Teleport = 13, AoeBomb = 14, AoeCluster = 15, BombFury = 16, HealSpawn = 17, Denial = 18 }