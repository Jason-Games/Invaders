using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienMaster : MonoBehaviour
{
    public AudioClip alienShootSfx;
    

    public GameObject bulletPrefab;
    public GameObject mothershipPrefab;

    public Vector2 spawnPos;

    private Vector3 motherShipSpawnPos = new Vector3(7, 3, 0);

    public Vector3 hMove = new Vector3(0.10f,0,0);
    public Vector3 VMove = new Vector3(0,0.35f,0);
    

    // Move boundaries
    private const float MaxLeft = -8f;
    private const float MaxRight = 8f;

    
    // Aliens speeds
    [Range(1,99)]
    public int startSpeed;
    [Range(1, 99)]
    public int endSpeed;
    
    private float moveTimerPerAlien;
    private float sSpeed;
    private float eSpeed;

    private const float YStartMovingPos = 0.85f;
    
    private float moveTimer;
    
    public float shootTime = 0.7f;
    private float shootTimer = 0f;

    
    // Mothership movement
    private float mothershipTimer = 5f;
    private const float MothershipMin = 15f;
    private const float MothershipMax = 60f;

    bool movingRight;

    bool entering = true;

    public static List<GameObject> aliens = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("Alien"))
            aliens.Add(go);

        // Calculate move timer

        sSpeed = 1 - (startSpeed / 100f);
        eSpeed = 1 - (endSpeed / 100f);

        moveTimerPerAlien = (sSpeed - eSpeed) / (float)aliens.Count;
        
        moveTimer = GetMoveSpeed();
    }

    public void SetShootSpeed(float shootSpeed)
    {
        var eb = bulletPrefab.GetComponent<EnemyBullet>();
        eb.speed = shootSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (entering)
        {
            transform.Translate(Vector2.down * Time.deltaTime * 10); 
            if (transform.position.y <= YStartMovingPos)
                entering = false;
        }
        else
        { 

            if (moveTimer <= 0)
                MoveEnemies();

            if (shootTimer <= 0)
                Shoot();

            if (mothershipTimer <= 0)
                SpawnMothership();

            moveTimer -= Time.deltaTime;
            shootTimer -= Time.deltaTime;
            mothershipTimer -= Time.deltaTime;
        }
    }

    private void Shoot()
    {
        if (aliens.Count <= 0) return;

        Vector2 pos = aliens[Random.Range(0, aliens.Count)].transform.position;

        Instantiate(bulletPrefab, pos, Quaternion.identity);
        AudioManager.PlaySoundEffect(alienShootSfx);
        shootTimer = GetShootTime();
    }

    private void SpawnMothership()
    {
        Instantiate(mothershipPrefab, motherShipSpawnPos, Quaternion.identity);
        mothershipTimer = Random.Range(MothershipMin,MothershipMax);

    }

    private void MoveEnemies()
    {
        if (aliens.Count > 0)
        {
            int hitMax = 0;

            for (int i = 0; i < aliens.Count; i++)
            {
                if (movingRight)
                    aliens[i].transform.position += hMove;
                else
                    aliens[i].transform.position -= hMove;

                if (aliens[i].transform.position.x <= MaxLeft || aliens[i].transform.position.x >= MaxRight)
                    hitMax++;
            }
            if (hitMax > 0)
            {
                for (int i = 0; i < aliens.Count; i++)
                {
                    aliens[i].transform.position -= VMove;
                }
                movingRight = !movingRight;
            }
            
        }
        
        moveTimer = GetMoveSpeed();
        AudioManager.UpdateBattleMusicDelay(moveTimer);


    }

    private float GetShootTime()
    {
        var r = Random.Range(0,100);

        if (r < 60) return shootTime;
        if (r < 80) return shootTime * 0.6f;
        return shootTime * 1.2f;

    }

    private float GetMoveSpeed()
    {

        float f = eSpeed + (aliens.Count * moveTimerPerAlien); 
 
       //Debug.Log("Timer:" + f);
        return f;
    }
}
