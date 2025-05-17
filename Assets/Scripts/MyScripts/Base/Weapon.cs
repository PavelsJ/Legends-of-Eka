using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Stats ownerStats;

    protected virtual void OnTriggerEnter(Collider other)
    {
        Stats targetStats = other.GetComponent<Stats>();
        
        if (targetStats != null && targetStats.isAlive)
        {
            DealDamage(targetStats);
        }
    }
    
    protected virtual void DealDamage(Stats target)
    {
        if (target == null || !target.isAlive) return;

        int damage = ownerStats.GetRandomDamage();
        target.TakeDamage(damage);
        
        BonusEffect(target);
    }
    
    protected virtual void BonusEffect(Stats target)
    {
        // switch (passiveType)
        // {
        //     case PassiveType.Critical:
        //         target.TakeDamage(GetRandomDamage() * critMultyplier);
        //         break;
        //
        //     case PassiveType.Bleed:
        //         StartCoroutine(ApplyBleed(target, bleedDamage,3, 1f));
        //         break;
        //
        //     case PassiveType.None:
        //     default:
        //         break;
        // }
    }
    
    private IEnumerator ApplyBleed(Stats target, int damage, int ticks, float delay)
    {
        for (int i = 0; i < ticks; i++)
        {
            if (target != null && target.isAlive)
            {
                target.TakeDamage(damage); 
            }
            yield return new WaitForSeconds(delay);
        }
    }

    public Stats GetOwner()
    {
        return ownerStats;
    }
}
