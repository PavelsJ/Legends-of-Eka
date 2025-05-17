using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : Weapon
{
    private void Awake()
    {
        if (ownerStats == null)
            ownerStats = GetComponentInParent<PlayerStats>();
    }
}
