using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienMaster : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject mothershipPrefab;

    private Vector3 hMove = new Vector3(0.05f,0,0);
    private Vector3 VMove = new Vector3(0,0.25f,0);
    private Vector3 motherShipSpawnPos = new Vector3(7,3,0);

    private const float MaxLeft = -8f;
    private const float MaxRight = 8f;
    private const float MaxMoveSpeed = 0.02f;
    private const float YStartMovingPos = 0.85f;
    private const float moveTime = 0.005f;
    private float moveTimer = 0f;
    
    private const float shootTime = 0.2f;
    private float shootTimer = 0f;
    
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
        Vector2 pos = aliens[Random.Range(0, aliens.Count-1)].transform.position;
        Instantiate(bulletPrefab, pos, Quaternion.identity);
        shootTimer = shootTime;
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
    }



    private float GetMoveSpeed()
    {
        float f = aliens.Count * moveTime; 

        if (f < MaxMoveSpeed)
            return MaxMoveSpeed;

        return f;
    }
}
