using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : CoreComponent
{
    public event Action OnHealthZero;

    [SerializeField] private float maxHealth;
    private float currentHealth;

    private HealthBar healthBarr => healthBar ? healthBar : core.GetCoreComponent(ref healthBar);
    private HealthBar healthBar;

    private void Start() {
        healthBar = GameObject.Find("Health bar").GetComponent<HealthBar>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
    protected override void Awake()
    {
        base.Awake();
        currentHealth = maxHealth;
    }

    public void DecreaseHealth(float amount)
    {
        currentHealth -= amount;
        healthBar.SetHealth(currentHealth);

        if(currentHealth <= 0)
        {
            currentHealth = 0;

            OnHealthZero?.Invoke();

            Debug.Log("Health is Zero!!");
        }
    }

    public void IncreaseHealth(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    }

    public void Reset()
    {
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);
    }
}
