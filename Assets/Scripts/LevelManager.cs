using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager 
{
    // Manages levels
    private static List<Level> Levels = new List<Level>();

    static LevelManager()
    {
        Levels.Add(new Level { AlienSet = 1, HorizontalMovement = 0.2f, VerticalMovement = 0.5f, StartSpeed = 10, EndSpeed = 40, ShootTime = 1f, ShootSpeed = 9f });
        Levels.Add(new Level { AlienSet = 1, HorizontalMovement = 0.3f, VerticalMovement = 0.5f, StartSpeed = 20, EndSpeed = 50, ShootTime = 0.9f, ShootSpeed = 9f });
        Levels.Add(new Level { AlienSet = 2, HorizontalMovement = 0.3f, VerticalMovement = 0.5f, StartSpeed = 20, EndSpeed = 50, ShootTime = 0.9f, ShootSpeed = 9f });
        Levels.Add(new Level { AlienSet = 4, HorizontalMovement = 0.6f, VerticalMovement = 0.3f, StartSpeed = 40, EndSpeed = 50, ShootTime = 1.9f, ShootSpeed = 4f });
        Levels.Add(new Level { AlienSet = 3, HorizontalMovement = 0.4f, VerticalMovement = 0.5f, StartSpeed = 20, EndSpeed = 50, ShootTime = 0.9f, ShootSpeed = 15f });
        Levels.Add(new Level { AlienSet = 5, HorizontalMovement = 0.6f, VerticalMovement = 0.3f, StartSpeed = 40, EndSpeed = 50, ShootTime = 1.9f, ShootSpeed = 4f });
        Levels.Add(new Level { AlienSet = 1, HorizontalMovement = 0.3f, VerticalMovement = 0.4f, StartSpeed = 20, EndSpeed = 70, ShootTime = 0.3f, ShootSpeed = 4f });
        Levels.Add(new Level { AlienSet = 4, HorizontalMovement = 0.5f, VerticalMovement = 0.4f, StartSpeed = 60, EndSpeed = 90, ShootTime = 0.9f, ShootSpeed = 15f });
        Levels.Add(new Level { AlienSet = 2, HorizontalMovement = 0.6f, VerticalMovement = 0.5f, StartSpeed = 30, EndSpeed = 50, ShootTime = 0.5f, ShootSpeed = 10f });


        // Top up with random generated levels

        for (int i = 0; i < 50; i++)
        {
           var l = new Level();
            l.AlienSet = Random.Range(1,6);
            l.HorizontalMovement = Random.Range(0.2f, 0.7f);
            l.VerticalMovement = Random.Range(0.3f, 0.9f);
            l.StartSpeed = Random.Range(30,50);
            l.EndSpeed = Random.Range(60,100);
            l.ShootTime = Random.Range(0.2f, 1.4f);
            l.ShootSpeed = Random.Range(4f,24f);
            Levels.Add(l);
        }

    }

    public static Level GetLevel(int wave)
    {
        if (wave > Levels.Count-1) return null;

        return Levels.ToArray()[wave];
    }

}

public class Level
{
    public int AlienSet;
    public float HorizontalMovement;
    public float VerticalMovement;

    public int StartSpeed;
    public int EndSpeed;
    public float ShootTime;
    public float ShootSpeed;
}
