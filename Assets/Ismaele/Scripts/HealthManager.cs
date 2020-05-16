using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthManager : MonoBehaviour
{
    public int Health { get; private set; }
    public int maxHealth = 100;

    public float HealthPercent { get => Health / maxHealth; }
    public bool IsAlive { get => Health > 0; }

    public Action onKilled;
    public Action<GameObject> onDamaged;
    public Action onHealed;
    public Action onRevived;

    private void Start()
    {
        Health = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        if (damageAmount <= 0)
            return;

        if (IsAlive)
        {
            Health -= damageAmount;
            Health = Mathf.Clamp(Health, 0, maxHealth);
        }

        if (!IsAlive)
        {
            onKilled?.Invoke();
        }
    }

    public void HealDamage(int healAmount)
    {
        if (healAmount <= 0)
            return;

        if (IsAlive)
        {
            Health += healAmount;
            Health = Mathf.Clamp(Health, 0, maxHealth);
        }
    }

    public void Revive(int reviveAmount)
    {
        if (reviveAmount <= 0)
            return;

        if (!IsAlive)
        {
            Health = reviveAmount;
            Health = Mathf.Clamp(Health, 0, maxHealth);
        }
    }
}