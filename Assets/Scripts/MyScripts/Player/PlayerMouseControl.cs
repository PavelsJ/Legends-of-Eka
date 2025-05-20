using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMouseControl : MonoBehaviour
{
    [Header("Movement")] 
    public Transform laserDot;
    public float moveSpeed = 10f;
    public LayerMask groundMask;
    
    private bool isDisabled = false;
    private bool isMoving = false;
    
    [Header("Stats")]
    public PlayerStats stats;
    
    [Header("Attack")]
    public Collider defCol;
    public Collider aoeCol;
    
    private enum AttackType { None, Normal, AoE }
    private bool isAttacking = false;
    
    private NavMeshAgent agent;
    private Animator animator;

    private void Awake()
    {
        DisableWeapon(AttackType.Normal);
        DisableWeapon(AttackType.AoE);
    }
    
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        
        agent.stoppingDistance = 0.1f;
        agent.speed = moveSpeed;
        
        laserDot.parent = null;

        if (stats == null)
        {
            stats = GetComponent<PlayerStats>();
        }
    }
    
    void Update()
    {
        if (!stats.isAlive)
        {
            return;
        }

        if (!isDisabled)
        {
            HandleInput();
            UpdateAnimation();
        }
    }

    private void HandleInput()
    {
        if (Input.GetMouseButton(1) && !isAttacking)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 500, groundMask))
            {
                agent.SetDestination(hit.point);
                laserDot.position = hit.point;
            }
        }

        if (!isAttacking && !stats.isHit)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                PerformAttack(AttackType.Normal);
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                PerformAttack(AttackType.AoE);
            }
        }
        else
        {
            StopMovement();
        }
    }

    private void PerformAttack(AttackType type)
    {
        if (stats.CurrentStamina < 20f) return;
        
        isAttacking = true;
        agent.ResetPath();

        stats.UseStamina(20f);

        switch (type)
        {
            case AttackType.Normal:
                animator.SetTrigger("Stab");
                break;

            case AttackType.AoE:
                animator.SetTrigger("AoE");
                break;
        }
    }
    
    private void UpdateAnimation()
    {
        isMoving = agent.velocity.magnitude > 0.1f;
        animator.SetBool("Run", isMoving);

        if (isMoving)
        {
            transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
        }
    }
    
    private void StopMovement()
    {
        if (agent.hasPath)
        {
            agent.ResetPath();
            animator.SetBool("Run", false);
        }
    }

    private void EnableWeapon(AttackType type)
    {
        switch (type)
        {
            case AttackType.Normal:
                if (defCol != null)
                    defCol.enabled = true;
                break;

            case AttackType.AoE:
                if (aoeCol != null)
                    aoeCol.enabled = true;
                break;
        }
    }

    private void DisableWeapon(AttackType type)
    {
        isAttacking = false;
        
        switch (type)
        {
            case AttackType.Normal:
                if (defCol != null)
                    defCol.enabled = false;
                break;

            case AttackType.AoE:
                if (aoeCol != null)
                    aoeCol.enabled = false;
                break;
        }
    }
}
