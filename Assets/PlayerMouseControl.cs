using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseControl : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 10f;
    public LayerMask layerMask;
    
    [Header("Attack")]
    public Collider weaponCol;
    
    private CharacterController characterController;
    private Vector3 targetPos;
    
    private Animator animator;
    
    private bool isRunning = false;
    private bool isAttacking = false;

    private void Awake()
    {
        DisableWeapon();
    }

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        
        targetPos = transform.position;
    }
    
    void Update()
    {
        if (!PlayerStats.isAlive)
        {
            return;
        }
        
        Movement();
        
        Attack();
    }

    private void Movement()
    {
        if (Input.GetMouseButton(1) && !isAttacking)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit, 500, layerMask))
            {
                targetPos = hit.point;
            }
        }
        
        if (Vector3.Distance(transform.position, targetPos) > 1f)
        {
            isRunning = true;
            Vector3 targetDir = Vector3.Normalize(targetPos - transform.position);
            characterController.Move(targetDir * (moveSpeed * Time.deltaTime));
            
            transform.LookAt(targetPos);
        }
        else
        {
            isRunning = false;
        }
        
        animator.SetBool("Run", isRunning);
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            animator.SetTrigger("Stab");
            isAttacking = true;
        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            isAttacking = false;
        }
        
        if (isAttacking)
        {
            targetPos = transform.position;
            isRunning = false;
        }
    }

    public void EnableWeapon()
    {
        if (weaponCol != null)
        {
            weaponCol.enabled = true;
        }
    }

    public void DisableWeapon()
    {
        if (weaponCol != null)
        {
            weaponCol.enabled = false;
        }
    }
}
