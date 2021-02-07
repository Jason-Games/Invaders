using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mothership : MonoBehaviour
{
    public AudioClip movingSfx;

    public int scoreValue;
    private const float maxLeft = -8f;
    private const float speed = 5f;

    float sfxTimer;

    private void Start()
    {
        AudioManager.PlaySoundEffect(movingSfx);
    }


    void PlaySound()
    {
        sfxTimer += Time.deltaTime;

        if (sfxTimer > 1f)
        {
            sfxTimer = 0;
            AudioManager.PlaySoundEffect(movingSfx);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * speed);

        PlaySound();

        if (transform.position.x <= maxLeft)
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("FriendlyBullet"))
        {
            UIManager.UpdateScore(scoreValue);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
