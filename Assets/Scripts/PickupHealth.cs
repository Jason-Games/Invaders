using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHealth : Pickups
{
    public override void PickMeUp()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().AddHealth();
        Destroy(gameObject);
    }
}
