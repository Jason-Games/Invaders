using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupLife : Pickups
{
    public override void PickMeUp()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().AddLife();
        Destroy(gameObject);
    }
}
