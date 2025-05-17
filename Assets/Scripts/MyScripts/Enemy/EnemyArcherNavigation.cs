using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyArcherNavigation : Enemy
{
    [Header("Arrow")]
    public GameObject arrowPrefab;
    public Transform arrowPoint;
    public float attackForce = 1;
    private enum AttackType { SpreadShot, TripleShot }

    protected override void HandleAttack()
    {
        AttackType attack = (AttackType)Random.Range(0, 2);
        switch (attack)
        {
            case AttackType.SpreadShot:
                animator.SetTrigger("Shoot1");
                break;
            case AttackType.TripleShot:
                animator.SetTrigger("Shoot2");
                break;
        }
    }
    
    private void ShootSpread()
    {
        float spreadAngle = 10f;

        for (int i = -1; i <= 1; i++)
        {
            Quaternion spreadRotation = Quaternion.Euler(0, i * spreadAngle, 0) * transform.rotation;
            SpawnArrow(spreadRotation);
        }
    }

    private void Shoot()
    {
        SpawnArrow(transform.rotation);
    }
    
    private void SpawnArrow(Quaternion rotation)
    {
        GameObject arrow = Instantiate(arrowPrefab, arrowPoint.position, rotation);
        if (arrow.TryGetComponent(out EnemyArrow arrowComponent))
        {
            arrowComponent.ownerStats = stats;
        }

        Rigidbody rb = arrow.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 dir = rotation * Vector3.forward;
            rb.AddForce(dir * attackForce, ForceMode.Impulse);
        }
    }
    
    private void EndAttackAfter()
    {
        isAttacking = false;
    }
}
