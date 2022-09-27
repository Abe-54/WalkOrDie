using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFinish : MonoBehaviour
{
    public int levelToComplete;
    public Collider2D nextConfiner;
    public Level nextLevel;

    public Door previousDoor;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.levels[0].isCompleted == true)
        {
            previousDoor.isOpen = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        gameManager.levels[levelToComplete].isCompleted = true;
        gameManager.currentLevel += 1;
        gameManager.levels.Add(nextLevel);
    }
}
