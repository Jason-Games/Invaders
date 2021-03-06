﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickups : MonoBehaviour
{
    public float fallSpeed;

    public AudioClip pickupSfx;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * Time.deltaTime * fallSpeed);
    }

    public abstract void PickMeUp();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AudioManager.PlaySoundEffect(pickupSfx);
            PickMeUp();
        }
    }

}
