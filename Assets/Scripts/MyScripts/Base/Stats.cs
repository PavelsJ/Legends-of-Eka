using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 100;
    private int currentHealth;
    public Image healthBar;
    
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
    public float attackLockTime = 1f;
    private float lastHitTime = -Mathf.Infinity;
    
    private Animator animator;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }
    
    protected internal virtual void TakeDamage(int damage)
    {
        if (!isAlive || Time.time < lastHitTime + invulnerableTime)
            return;

        lastHitTime = Time.time;
        currentHealth -= damage;
        
        currentHealth = Mathf.Max(currentHealth, 0);
        UpdateHealthBar();

        animator.SetTrigger("Hurt");
        Debug.Log($"{gameObject.name} current health: {currentHealth}/{maxHealth}.");
        
        if (!isHit)
        {
            StartCoroutine(TemporarilyDisableAttack());
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    private IEnumerator TemporarilyDisableAttack()
    {
        isHit = true;
        yield return new WaitForSeconds(attackLockTime);
        isHit = false;
    }
    
    public virtual void TakeHeal(int healAmount)
    {
        if (!isAlive) return;

        currentHealth = Mathf.Min(currentHealth + healAmount, maxHealth);
        UpdateHealthBar();
    }
    
    protected virtual void Die()
    {
        isAlive = false;
        
        animator.SetTrigger("Death");
        
        Debug.Log($"{gameObject.name} died.");
    }

    internal int GetRandomDamage()
    {
        return Random.Range(minDamage, maxDamage);
    }
    
    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    internal bool IsAlive()
    {
        return isAlive;
    }
    
    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = (float)currentHealth / maxHealth;
        }
    }
}
