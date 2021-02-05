using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveObject
{
    public int coins;
    public int highScore;
    public ShipStats shipStats;

    public SaveObject()
    {
        this.shipStats = new ShipStats();
        this.shipStats.maxHealth = 5;
        this.shipStats.maxLives = 3;
        this.shipStats.shipSpeed = 4;
        this.shipStats.fireRate = 0.2f;
    }
}
