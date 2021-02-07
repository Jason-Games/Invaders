using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheild : MonoBehaviour
{
    public AudioClip explosionSfx;
    public Sprite[] states;

    private int health;
    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();

    }

    // Start is called before the first frame update
    void Start()
    {
        health = 4;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            Destroy(collision.gameObject);
            health--;

            AudioManager.PlaySoundEffect(explosionSfx);

            if (health <= 0)
            {
                Destroy(gameObject);
                //gameObject.SetActive(false);
            
            } 
            else
            {
                sr.sprite = states[health - 1];
            }

        }
    }



}