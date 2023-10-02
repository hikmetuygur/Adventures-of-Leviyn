using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private AudioClip pickupSFX;
    [SerializeField] private int scoreValue = 100;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(pickupSFX, Camera.main.transform.position);
            FindObjectOfType<GameSession>().AddToScore(scoreValue);
            Destroy(gameObject);
        }
    }
}
