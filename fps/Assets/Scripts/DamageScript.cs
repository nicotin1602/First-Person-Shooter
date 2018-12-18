using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour {

    private int actualDamage;

    private float distanceMultiplier;

    public int minDistance;

    public int calculateDamage(Item weapon, float distance)
    {
        if (distance < minDistance)
        {
            actualDamage = weapon.damage;
        }
        else
        {
            distanceMultiplier = weapon.range / distance / 100f;
            if (distanceMultiplier > 1f)
            {
                distanceMultiplier = 1f;
            }

            actualDamage = Mathf.RoundToInt (weapon.damage * distanceMultiplier); 
            
        }
        return actualDamage;
    }


}
