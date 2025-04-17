using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    
    public float invulnerableTime = 0.2f;
    private float lastHitTimer = 0;

    public static bool isAlive = true;
    private Animator animator;
    
    public int CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            if (value < 0) { currentHealth = 0; }
            else { currentHealth = value; }
        }
    }
    
    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyWeapon") && isAlive)
        {
            if (currentHealth <= 0 )
            {
                isAlive = false;
                animator.SetTrigger("Death");
                return;
            }
            
            TakeDamage(5);
        }
    }

    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth);
        animator.SetTrigger("HitBack");
    }
}
