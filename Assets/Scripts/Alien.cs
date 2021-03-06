﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
    public AudioClip explosionSfx;

    public int scoreValue;

    public GameObject explosion;
    
    public GameObject coinPrefab;
    public GameObject lifePrefab;
    public GameObject healthPrefab;

    private const int coinChance = 150;
    private const int lifeChance = 1;
    private const int healthChance = 50;

    private float playerYPos = -4f;

    public void Kill()
    {
        UIManager.UpdateScore(scoreValue);
        AlienMaster.aliens.Remove(gameObject);

        int ran = Random.Range(0,1000);

        if (ran == lifeChance)
            Instantiate(lifePrefab, transform.position, Quaternion.identity);
        else if (ran <= healthChance)
            Instantiate(healthPrefab, transform.position, Quaternion.identity);
        else if (ran <= coinChance)
            Instantiate(coinPrefab, transform.position, Quaternion.identity);

        Instantiate(explosion, transform.position, Quaternion.identity);

        AudioManager.PlaySoundEffect(explosionSfx);

        AudioManager.UpdateBattleMusicDelay(AlienMaster.aliens.Count);

        if (AlienMaster.aliens.Count == 0)
            GameManager.SpawnNewWave();

        Destroy(gameObject);
    }

    private void Update()
    {
        if (transform.position.y <= playerYPos)
        {            
           GameManager.GameOver();
        }
    }
}
