using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public Timer[] timers = new Timer[2];
    private GameManager gameManager;

    public int initialTimeForNextLevel;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        for (int i = 0; i < timers.Length; i++)
        {
            gameManager.ResetTimer(timers[i], initialTimeForNextLevel);
        }

        gameObject.SetActive(false);
    }
}
