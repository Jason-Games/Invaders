﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
   
    public TextMeshProUGUI scoreText;
    private int score;
    
    public TextMeshProUGUI highScoreText;
    private int highSscore;

    public TextMeshProUGUI coinsText;
    private int coins;

    public TextMeshProUGUI waveText;
    private int waves;

    public Image[] lifeSprites;
    public Image heathBar;

    public Sprite[] healthBars;

    private Color active = new Color(1,1,1,1);
    private Color inactive = new Color(1,1,1,0.25f);


    private static UIManager instance;

    private void Awake()
    {
        if (instance == null)    
            instance = this;
        else
            Destroy(gameObject);
    }

    public static void UpdateLives(int lives)
    {
        foreach(Image i in instance.lifeSprites)
        {
            i.color = instance.inactive;
        }

        for (int i = 0; i < lives; i++)
        {
            instance.lifeSprites[i].color = instance.active;
        }
    }

    public static void UpdateHealthBar(int health)
    {
        instance.heathBar.sprite = instance.healthBars[health];
    }

    public static void UpdateScore(int score)
    {
        instance.score += score;
        instance.scoreText.text = instance.score.ToString("###,###");
    }
    public static void UpdateHighScore(int highScore)
    {
        instance.highSscore = highScore;
        instance.highScoreText.text = instance.highSscore.ToString("000,000");
    }

    public static int GetHighScore()
    {
        return instance.highSscore;
    }

    public static void UpdateWaves()
    {
        instance.waves++;
        instance.waveText.text = instance.waves.ToString();

    }
    public static void UpdateCoins()
    {
        instance.coinsText.text = Inventory.currentCoins.ToString();
    }

    public static void ResetUI()
    {
        instance.score = 0;
        instance.waves = -1;
        UpdateScore(0);
        UpdateWaves();

    }

 }