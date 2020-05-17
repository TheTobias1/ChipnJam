using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public static bool abilityActive;

    public ProjectileSpawner[] Spawners;
    public float fireRate;

    public WeaponType weaponType;

    private float nextShot;

    private void Start()
    {
        SpellSpawner sp = GetComponentInParent<SpellSpawner>();
        if(sp != null)
        {
            if(sp.currentWeapon != null && weaponType == WeaponType.Attack)
            {
                Destroy(sp.currentWeapon.gameObject);
                sp.currentWeapon = this;
            }

            if (sp.currentWeapon != null && weaponType == WeaponType.Ability)
            {
                Destroy(sp.currentAbility.gameObject);
                sp.currentAbility = this;
            }
        }

        if(weaponType == WeaponType.Ability)
        {
            Weapon.abilityActive = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(weaponType != WeaponType.Custom)
        {
            if(Time.time > nextShot)
            {
                if(InputManager.inputBuffer.ability && weaponType == WeaponType.Ability && Weapon.abilityActive)
                {
                    Attack();
                    Weapon.abilityActive = false;
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

public enum WeaponType { Attack, Ability, Movement, Custom };
