using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : Weapon
{
    protected override void OnTriggerEnter(Collider other)
    {
        Stats targetStats = other.GetComponent<Stats>();
        
        if (targetStats != null && targetStats.isAlive)
        {
            DealDamage(targetStats);
        }
    }
}
