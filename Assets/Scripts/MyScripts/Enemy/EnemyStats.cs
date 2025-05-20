using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : Stats
{
    public event Action OnDeath;
    
    [Header("Exp Amount")]
    public int expAmount = 20;
    private UI_Manager manager;
    
    [Header("UI Elements")]
    public Image healthBar;

    private void Start()
    {
        manager = FindObjectOfType<UI_Manager>();
    }

    private void OnEnable()
    {
        if (healthBar != null)
        {
           OnHealthChanged += UpdateHealthBar;
        }
    }

    private void OnDisable()
    {
        if (healthBar != null)
        {
           OnHealthChanged -= UpdateHealthBar;
        }
    }
    
    private void UpdateHealthBar(float current, float max)
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = current / max;
        }
    }
    
    protected override void Die()
    {
        base.Die();
        OnDeath?.Invoke();
        
        manager.GainExp(expAmount);
       
        Destroy(gameObject, 2f);
    }
}
