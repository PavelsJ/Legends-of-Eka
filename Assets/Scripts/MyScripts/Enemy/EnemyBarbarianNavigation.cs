using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyBarbarianNavigation : Enemy
{
    [Header("Sword")]
    public Collider defCol;
    private enum AttackType { Normal, TripleAttack }

    private void Awake()
    {
        DisableWeapon();
    }

    protected override void HandleAttack()
    {
        AttackType attack = (AttackType)Random.Range(0, 2);
        switch (attack)
        {
            case AttackType.Normal:
                animator.SetTrigger("Attack1");
                break;
            case AttackType.TripleAttack:
                animator.SetTrigger("Attack2");
                break;
        }
    }
    
    private void EnableWeapon()
    {
        defCol.enabled = true;
    }

    private void DisableWeapon()
    {
        isAttacking = false;
        defCol.enabled = false;
    }
}
