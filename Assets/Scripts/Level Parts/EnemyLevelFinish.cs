using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLevelFinish : MonoBehaviour
{
    public List<Enemy> totalEnemies;

    public int enemiesDefeated;
    public int totalEnemiesToKill;

    public Door doorToOpen;

    // Start is called before the first frame update
    void Start()
    {
        totalEnemiesToKill = totalEnemies.Count;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemiesDefeated == totalEnemiesToKill)
        {
            doorToOpen.isOpen = true;
        }
    }
}
