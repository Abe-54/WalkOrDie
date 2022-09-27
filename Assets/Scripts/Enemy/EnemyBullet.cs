using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float bulletSpeed;

    // References
    private Rigidbody2D rb2d;
    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb2d.velocity = transform.right * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Enemy enemy = FindObjectOfType<Enemy>();

        if (other.gameObject.tag == "Player")
        {
            player = other.gameObject.GetComponent<PlayerController>();

            StartCoroutine(player.Die());
            player.gameObject.SetActive(false);
        }

        enemy.bulletPool.Release(gameObject);
    }
}
