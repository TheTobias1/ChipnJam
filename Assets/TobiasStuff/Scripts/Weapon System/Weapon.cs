using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public ProjectileSpawner[] Spawners;
    public float fireRate;

    public WeaponType weaponType;

    private float nextShot;

    // Update is called once per frame
    void Update()
    {
        if(weaponType != WeaponType.Custom)
        {
            if(Time.time > nextShot)
            {
                if(InputManager.inputBuffer.ability && weaponType == WeaponType.Ability)
                {
                    Attack();
                }

                if(InputManager.inputBuffer.attack && weaponType == WeaponType.Attack)
                {
                    Attack();
                }  
            }

        }
    }

    void Attack()
    {
        nextShot = Time.time + fireRate;
        foreach (ProjectileSpawner s in Spawners)
        {
            s.TriggerSpawn();
        }
    }
}

public enum WeaponType { Attack, Ability, Custom };
