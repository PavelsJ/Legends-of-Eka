using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelthMushroom : Item
{
    public int health = 20;

    protected internal override void UseItem(GameObject entity)
    {
        Stats stats = entity.GetComponent<Stats>();
        if (stats != null)
        {
            stats.TakeHeal(health);
            Debug.Log(entity.name + " takes " + health + " health");
        }
        
        Destroy(gameObject);
    }
    
}
