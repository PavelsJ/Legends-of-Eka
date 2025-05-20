using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Stats : MonoBehaviour
{
    public event Action<float, float> OnHealthChanged;

    public int CurrentHealth
    {
        get => currentHealth;
        set
        {
            currentHealth = Mathf.Clamp(value, 0, maxHealth);
            OnHealthChanged?.Invoke(currentHealth, maxHealth);
        }
    }
    
    [Header("Health")]
    public int maxHealth = 100;
    private int currentHealth;
    
    internal bool isAlive = true;
    internal bool isHit = false;    
    
    [Header("Damage")]
    public int maxDamage = 10;
    public int minDamage = 5;
    
    [Header("Passive")]
    public PassiveType passiveType = PassiveType.None;
    
    public int critMultyplier = 2;
    public int bleedDamage = 2;
    public enum PassiveType { None, Critical, Bleed }
    
    [Header("Invulnerability")]
    public float invulnerableTime = 0.2f;
    public float stunTime = 1f;
    
    private Coroutine attackLockCoroutine;
    private float lastHitTime = -Mathf.Infinity;

    private Animator animator;
    
    void Awake()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        
        OnHealthChanged?.Invoke(CurrentHealth, maxHealth);
    }
    
    protected internal virtual void TakeDamage(int damage)
    {
        if (!isAlive || Time.time < lastHitTime + invulnerableTime)
            return;

        lastHitTime = Time.time;
        CurrentHealth -= damage;
        
        animator.SetTrigger("Hurt");
        Debug.Log($"{gameObject.name} current health: {currentHealth}/{maxHealth}.");
        
        if (currentHealth <= 0)
        {
            Die();
        }
        
        if (!isHit)
        {
            if (attackLockCoroutine == null)
            {
                attackLockCoroutine = StartCoroutine(TemporarilyDisableAttack());
            }
        }
    }

    private IEnumerator TemporarilyDisableAttack()
    {
        isHit = true;
        yield return new WaitForSeconds(stunTime);
        isHit = false;
        
        attackLockCoroutine = null;
    }
    
    public virtual void TakeHeal(int healAmount)
    {
        if (!isAlive) return;

        CurrentHealth = Mathf.Min(currentHealth + healAmount, maxHealth);
    }
    
    protected virtual void Die()
    {
        isAlive = false;
        CurrentHealth = 0;
        
        animator.SetTrigger("Death");
        Debug.Log($"{gameObject.name} died.");
    }

    internal int GetRandomDamage()
    {
        return Random.Range(minDamage, maxDamage);
    }
}
