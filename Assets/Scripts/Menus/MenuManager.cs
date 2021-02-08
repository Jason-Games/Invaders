using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public AudioSource mainMenuSong;

    public GameObject mainMenu;
    public GameObject gameOverMenu;
    public GameObject inGameMenu;
    public GameObject pauseMenu;

    public AudioClip gameOverSfx;

    public TextMeshProUGUI scoreText;

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
        //instance.mainMenuSong.Play();
        instance.mainMenuSong.volume = 0.6f;
        instance.inGameMenu.SetActive(false);
    }
    public static void OpenGameOverMenu()
    {
        AudioManager.PlaySoundEffect(instance.gameOverSfx);
        instance.scoreText.text = UIManager.GetScore().ToString("###,###");
        Time.timeScale = 0;
        instance.gameOverMenu.SetActive(true);
        
        instance.inGameMenu.SetActive(false);

        AudioManager.PlaySoundEffect(instance.gameOverSfx);
    }

    public void StartNewGame()
    {
        //instance.mainMenuSong.Stop();
        instance.mainMenuSong.volume = 0.3f;
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
