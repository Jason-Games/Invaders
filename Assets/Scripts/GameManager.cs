using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] alientSets;

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
        GameManager.SpawnNewWave();
    }

    public static void SpawnNewWave()
    {
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
