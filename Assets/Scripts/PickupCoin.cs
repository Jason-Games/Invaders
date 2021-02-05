using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCoin : Pickups
{
    public override void PickMeUp()
    { 
        Inventory.currentCoins++;
        UIManager.UpdateCoins();
        Destroy(gameObject);
    }
}
