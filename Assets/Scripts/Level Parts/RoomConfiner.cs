using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomConfiner : MonoBehaviour
{
    public Collider2D roomConfiner;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        gameManager.currentCameraConfiner = roomConfiner;
    }
}
