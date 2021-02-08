using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    

    private void Awake()
    {
        if (SaveManager.instance == null)
            SaveManager.instance = this;
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadProgress();
    }

    public static void SaveProgress()
    { 
        SaveObject so = new SaveObject();
        //so.coins = Inventory.currentCoins;
        so.highScore = UIManager.GetHighScore();
        //so.shipStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().shipStats;

        SaveLoad.SaveState(so);
    }

    public static void LoadProgress()
    {
        SaveObject so = SaveLoad.LoadState();
        //Inventory.currentCoins = so.coins;
        UIManager.UpdateHighScore(so.highScore);
        
        //GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().shipStats = so.shipStats;

    }
}
