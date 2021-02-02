using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
   public int scoreValue;

    public GameObject explosion;

    public void Kill()
    {
        UIManager.UpdateScore(scoreValue);
        AlienMaster.aliens.Remove(gameObject);

        Instantiate(explosion, transform.position, Quaternion.identity);
        if (AlienMaster.aliens.Count == 0)
            GameManager.SpawnNewWave();

        Destroy(gameObject);
    }

}
