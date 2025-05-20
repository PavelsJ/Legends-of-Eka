using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Movement")]
    public Transform target;
    public float attackDistance = 0;
    
    [Header("Stats")]
    public EnemyStats stats;

    [Header("Attack")] 
    public float attackCooldown = 2;  
    private float lastAttackTime = 0;
    internal bool isAttacking = false;
    
    private NavMeshAgent agent;
    internal Animator animator;
    
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        if (stats == null)
            stats = GetComponent<EnemyStats>();
    }

    private void Update()
    {
        if(target == null) return;
        
        if (!stats.isAlive)
        {
            agent.isStopped = true;
            return;
        }

        float distToTarget = Vector3.Distance(transform.position, target.position);

        if (!isAttacking && !stats.isHit)
        {
            if (distToTarget <= attackDistance && Time.time > lastAttackTime + attackCooldown)
            {
                agent.isStopped = true;
                isAttacking = true;
                lastAttackTime = Time.time;

                HandleAttack();
            }
            else
            {
                agent.isStopped = false;
                agent.SetDestination(target.position);
            }

            animator.SetFloat("Speed", agent.velocity.magnitude);
        }
        else
        {
            StopMovement();
        }

        if (distToTarget <= attackDistance)
        {
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
        }
    }
    
    private void StopMovement()
    {
        if (agent.hasPath)
        {
            agent.ResetPath();
            animator.SetFloat("Speed", 0);
        }
    }

    protected virtual void HandleAttack()
    {
        
    }
}
