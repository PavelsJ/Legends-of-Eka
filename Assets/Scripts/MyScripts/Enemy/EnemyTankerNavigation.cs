using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTankerNavigation : Enemy
{
    [Header("Hammer")]
    public Collider[] defCols;
    public Collider aoeCol;
    private enum AttackType { Normal, SmashAttack }
    
    private void Awake()
    {
        DisableAll();
    }

    protected override void HandleAttack()
    {
        AttackType attack = (AttackType)Random.Range(0, 2);
        switch (attack)
        {
            case AttackType.Normal:
                animator.SetTrigger("Attack1");
                break;
            case AttackType.SmashAttack:
                animator.SetTrigger("Attack2");
                break;
        }
    }
    
    private void EnableWeapon(int index)
    {
        defCols[index].enabled = true;
    }

    private void DisableWeapon(int index)
    {
        defCols[index].enabled = false;
    }

    private void EnableSmash()
    {
        aoeCol.enabled = true;
    }

    private void DisableSmash()
    {
        aoeCol.enabled = false;
    }
    
    private void EndAttackAfter()
    {
        isAttacking = false;
    }

    private void DisableAll()
    {
        isAttacking = false;
        
        foreach (Collider col in defCols)
        {
            col.enabled = false;
        }
        aoeCol.enabled = false;
    }
}
