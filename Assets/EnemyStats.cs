using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    
    public float invulnerableTime = 0.2f;
    private float lastHitTimer = 0;

    public static bool isAlive = true;
    private Animator animator;
    
    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerWeapon") && isAlive)
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
        animator.SetTrigger("Hurt");
        
        Debug.Log("Enemy: " + currentHealth);
    }
}
