using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : Stats
{
    public event Action<float, float> OnStaminaChanged;
    public event Action OnPlayerDied;
    
    public float CurrentStamina
    {
        get => currentStamina;
        set
        {
            currentStamina = Mathf.Clamp(value, 0, maxStamina);
            OnStaminaChanged?.Invoke(currentStamina, maxStamina);
        }
    }
    
    [Header("Stamina")]
    public float maxStamina = 100f;
    private float currentStamina = 100f;
    
    public float staminaRegenRate = 15f;
    public float staminaRegenDelay = 1f;

    private Coroutine staminaRegenCoroutine;
    
    private void Start()
    {
        OnStaminaChanged?.Invoke(CurrentStamina, maxStamina);
        OnHealthChanged += HandleHealthChanged;
    }
    
    private void HandleHealthChanged(float current, float max)
    {
        if (!isAlive)
        {
            OnPlayerDied?.Invoke(); 
        }
    }
    
    public void UseStamina(float amount)
    {
        CurrentStamina -= amount;

        if (staminaRegenCoroutine != null)
        {
            StopCoroutine(staminaRegenCoroutine);
        }
        
        staminaRegenCoroutine = StartCoroutine(RegenerateStamina());
    }

    private IEnumerator RegenerateStamina()
    {
        yield return new WaitForSeconds(staminaRegenDelay);

        while (CurrentStamina < maxStamina)
        {
            CurrentStamina += staminaRegenRate * Time.deltaTime;
            yield return null;
        }

        staminaRegenCoroutine = null;
    }

    public void LevelUp()
    {
        maxHealth += 20;
        maxStamina += 20;
            
        CurrentHealth = maxHealth;
        CurrentStamina = maxStamina;
    }
}
