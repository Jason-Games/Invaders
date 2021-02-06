using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] alientSets;
    public GameObject shieldPrefab;

    private GameObject currentSet;
    private Vector2 spawnPos = new Vector2(0,4.5f);

    private static GameManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
    }

    public static void SetupNewGame()
    {

        
    }

    public static void CancelGame()
    {
        instance.StopAllCoroutines();

        AlienMaster.aliens.Clear();

        if (instance.currentSet != null)
            Destroy(instance.currentSet);

        UIManager.ResetUI();

        // Reset shields
        ResetSheilds();
        ResetProgress();
    }

    public static void ResetProgress()
    {
        GameManager.FindObjectOfType<Player>().GetComponent<Player>().Reset();
    }

    public static void CreateSheilds()
    {
        var leftSheildPos = new Vector2(-5.48f,-3.12f);
        var centerSheildPos = new Vector2(0f,-3.12f);
        var rightSheildPos = new Vector2(5.48f,-3.12f);

        Instantiate(instance.shieldPrefab, leftSheildPos, Quaternion.identity);
        Instantiate(instance.shieldPrefab, centerSheildPos, Quaternion.identity);
        Instantiate(instance.shieldPrefab, rightSheildPos, Quaternion.identity);
    }

    public static void ResetSheilds()
    {
        GameObject[] sheilds = GameObject.FindGameObjectsWithTag("ShieldMaster");

        foreach (var sheild in sheilds)
        {
            Destroy(sheild.gameObject);
        }

        CreateSheilds();
    }

    public static void SpawnNewWave()
    {
        AlienMaster.aliens.Clear();

        instance.StartCoroutine(instance.SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        if (currentSet != null)
            Destroy(currentSet);

        yield return new WaitForSeconds(3);

        currentSet = Instantiate(alientSets[Random.Range(0, alientSets.Length)], spawnPos, Quaternion.identity);

        UIManager.UpdateWaves();

    }
}
