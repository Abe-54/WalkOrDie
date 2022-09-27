using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float bulletSpeed;

    // References
    private Rigidbody2D rb2d;
    private EnemyLevelFinish enemyLevelFinish;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        enemyLevelFinish = FindObjectOfType<EnemyLevelFinish>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb2d.velocity = transform.right * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        player.bulletPool.Release(gameObject);

        if (other.gameObject.tag == "Enemy")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();

            for (var i = 0; i < player.timers.Length; i++)
            {
                player.timers[i].time += 2;
            }
            enemyLevelFinish.enemiesDefeated += 1;
            enemy.dieSound.Play();
            other.gameObject.SetActive(false);
        }
    }
}
