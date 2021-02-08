using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public GameObject explosion;

    public AudioClip shootSfx;
    public AudioClip damageSfx;
    public AudioClip dieSfx;

    public ShipStats shipStats;

    public GameObject bulletPrefab;
    
    private const float maxLeft = -8.5f;
    private const float maxRight = 8.5f;

    private Vector2 offScreen = new Vector2(0, -20f);
    private Vector2 startPos = new Vector2(0, -4f);

    private bool isShooting = false;

    private bool isPaused = false;
    

    // Start is called before the first frame update
    void Start()
    {
        shipStats.currentHealth = shipStats.maxHealth;
        shipStats.currentLives = shipStats.maxLives;

        UIManager.UpdateHealthBar(shipStats.currentHealth);
        UIManager.UpdateLives(shipStats.currentLives);
    }


    public void AddLife()
    {
        if (shipStats.currentLives == shipStats.maxLives)
        {
            UIManager.UpdateHighScore(1000);
        }
        else
        {
            shipStats.currentLives++;
            UIManager.UpdateLives(shipStats.currentLives);
        }


    }

    public void AddHealth()
    {
        if (shipStats.currentHealth == shipStats.maxHealth)
        {
            UIManager.UpdateScore(250);
        }
        else
        {
            shipStats.currentHealth++;
            UIManager.UpdateHealthBar(shipStats.currentHealth);
        }

    }
    private void TakeDamage()
    {
        shipStats.currentHealth--;
        
        

        if (shipStats.currentHealth <= 0)
        {
            shipStats.currentLives--;
            UIManager.UpdateLives(shipStats.currentLives);

            if (shipStats.currentLives <=0 )
            {
                GameManager.GameOver();
            }
            else
            {
                Debug.Log("Respwan");
                StartCoroutine(Respwan());
            }
        } else
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            AudioManager.PlaySoundEffect(damageSfx);
            UIManager.UpdateHealthBar(shipStats.currentHealth);
        }
    }

  

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                isPaused = true;
                MenuManager.OpenPauseMenu();
            } else
            {
                isPaused = false;
                MenuManager.instance.ClosePauseMenu();
            }
        }
            

    }

    private IEnumerator Respwan()
    {
        // Stop dead shooting
        AudioManager.PlaySoundEffect(dieSfx);
        isShooting = true;
        transform.position = offScreen;            
        yield return new WaitForSeconds(2);
        shipStats.currentHealth = shipStats.maxHealth;
        transform.position = startPos;
        isShooting = false;
        UIManager.UpdateHealthBar(shipStats.currentHealth);
        UIManager.UpdateLives(shipStats.currentLives);

    }

    private IEnumerator Shoot()
    {
        isShooting = true;
        
        Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        AudioManager.PlaySoundEffect(shootSfx);
        yield return new WaitForSeconds(shipStats.fireRate);
        isShooting = false;
    }

    public void Reset()
    {
        shipStats.currentHealth = shipStats.maxHealth;
        shipStats.currentLives = shipStats.maxLives;
        UIManager.UpdateHealthBar(shipStats.currentHealth);
        UIManager.UpdateLives(shipStats.currentLives);


    }
}
