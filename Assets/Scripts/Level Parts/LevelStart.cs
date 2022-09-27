using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStart : MonoBehaviour
{
    public Collider2D roomConfiner;
    private GameManager gameManager;

    public Door[] levelDoors;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(closeRoom());
        }
    }

    IEnumerator closeRoom()
    {
        gameManager.currentCameraConfiner = roomConfiner;

        for (int i = 0; i < levelDoors.Length; i++)
        {
            levelDoors[i].isOpen = false;
        }
        yield return new WaitForSeconds(1f);
    }
}
