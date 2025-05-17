using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArrow : Weapon
{
    private Coroutine coroutine;

    private void OnEnable()
    {
        coroutine = StartCoroutine(CurCoroutine());
    }
    
    protected override void OnTriggerEnter(Collider other)
    {
        Stats targetStats = other.GetComponent<Stats>();
        
        if (targetStats != null && targetStats.isAlive)
        {
            DealDamage(targetStats);
            DeactivateArrow();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        DeactivateArrow();
    }

    private void DeactivateArrow()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        StopCoroutine(coroutine);
        Destroy(gameObject);
    }

    private IEnumerator CurCoroutine()
    {
        yield return new WaitForSeconds(2f);
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}
