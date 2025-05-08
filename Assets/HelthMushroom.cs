using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelthMushroom : MonoBehaviour
{
    public int health = 20;

    public void UseItem()
    {
        PlayerStats.OnHeal(health);
        Destroy(gameObject);
    }
}
