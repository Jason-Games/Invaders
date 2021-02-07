using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public AudioSource mainMenuSong;

    public GameObject mainMenu;
    public GameObject gameOverMenu;
    public GameObject inGameMenu;
    public GameObject pauseMenu;

    public static MenuManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        ReturnToMainMenu();
    }

    public void OpenMainMenu()
    {
        instance.mainMenu.SetActive(true);
        instance.mainMenuSong.Play();
        instance.inGameMenu.SetActive(false);
    }
    public static void OpenGameOverMenu()
    {
        instance.gameOverMenu.SetActive(true);
        
        instance.inGameMenu.SetActive(false);
    }

    public void StartNewGame()
    {
        instance.mainMenuSong.Stop();
        Time.timeScale = 1;
        instance.mainMenu.SetActive(false);
        instance.pauseMenu.SetActive(false);
        instance.gameOverMenu.SetActive(false);
        instance.inGameMenu.SetActive(true);

        GameManager.SetupNewGame();
        GameManager.SpawnNewWave();

    }

    public static void OpenPauseMenu()
    {
        Time.timeScale = 0;
        instance.pauseMenu.SetActive(true);
        instance.inGameMenu.SetActive(false);
    }

    public void ClosePauseMenu()
    {
        Time.timeScale = 1;
        instance.pauseMenu.SetActive(false);
        instance.inGameMenu.SetActive(true);
    }


    public void ReturnToMainMenu()
    {
        Time.timeScale = 1;
        instance.pauseMenu.SetActive(false);
        instance.gameOverMenu.SetActive(false);
        instance.inGameMenu.SetActive(false);
        instance.mainMenuSong.Play();
        instance.mainMenu.SetActive(true);
        GameManager.CancelGame();
    }
    public static void CloseWindow(GameObject go)
    {
        go.SetActive(false);
    }

    
}
