using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public ShipStats shipStats;

    public GameObject bulletPrefab;
    
    private const float maxLeft = -8.5f;
    private const float maxRight = 8.5f;

    private Vector2 offScreen = new Vector2(0, -20f);
    private Vector2 startPos = new Vector2(0, -4f);

    private bool isShooting = false;
    

    // Start is called before the first frame update
    void Start()
    {
        shipStats.currentHealth = shipStats.maxHealth;
        shipStats.currentLives = shipStats.maxLives;
    }

    private void TakeDamage()
    {
        shipStats.currentHealth--;

        if (shipStats.currentHealth <= 0)
        {
            shipStats.currentLives--;

            if (shipStats.currentLives <=0 )
            {
                Debug.Log("Game Over");
                // Game Over
            }
            else
            {
                Debug.Log("Respwan");
                StartCoroutine(Respwan());
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            Debug.Log("Dead");
            TakeDamage();

            Destroy(collision.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        var h = Input.GetAxis("Horizontal");

        var newPlayerPos = Vector2.right * Time.deltaTime * shipStats.shipSpeed * h;

        if (transform.position.x + newPlayerPos.x >= maxLeft && transform.position.x + newPlayerPos.x <= maxRight)
            transform.Translate(newPlayerPos);

        if (Input.GetKeyDown(KeyCode.Space) && !isShooting)
            StartCoroutine(Shoot());

    }

    private IEnumerator Respwan()
    {
        transform.position = offScreen;            
        yield return new WaitForSeconds(2);
        shipStats.currentHealth = shipStats.maxHealth;
        transform.position = startPos;
    }

    private IEnumerator Shoot()
    {
        isShooting = true;
        Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(shipStats.fireRate);
        isShooting = false;
    }
}
