using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Enemy : MonoBehaviour
{
    public bool isDead;

    public float moveSpeed;

    public int direction = 1;

    [SerializeField] private LayerMask raycastLayerMask;
    [SerializeField] private LayerMask playerRaycastLayerMask;

    [SerializeField] private Vector2 raycastOffest;
    [SerializeField] private float raycastLengthLedge = 2f;
    [SerializeField] private float raycastLengthWall = 2f;
    [SerializeField] private float raycastLengthLineOfSight = 2f;

    [SerializeField] private Transform shootingPosition;
    [SerializeField] private float timeBetweenShots = 1f;
    [SerializeField] private float fireRate = 3f;

    [SerializeField] private GameObject bulletPrefab;
    public ObjectPool<GameObject> bulletPool;

    private RaycastHit2D rightLedgeRaycast;
    private RaycastHit2D leftLedgeRaycast;
    private RaycastHit2D rightWallRaycast;
    private RaycastHit2D leftWallRaycast;

    private RaycastHit2D lineOfSight;

    //Sounds
    public AudioSource shootSound;
    public AudioSource dieSound;

    public GameObject spotPlayerAnnouncer;

    private PlayerController player;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();

        spotPlayerAnnouncer.SetActive(false);

        //Bullet Object Pool
        bulletPool = new ObjectPool<GameObject>(() =>
        {
            return Instantiate(bulletPrefab);
        }, bullet =>
        {
            bullet.gameObject.SetActive(true);
        }, bullet =>
        {
            bullet.gameObject.SetActive(false);
        }, bullet =>
        {
            Destroy(bullet.gameObject);
        }, true, 10, 20);
    }

    private Vector2 moveDirection = Vector2.right;

    void Update()
    {
        #region Movement

        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);

        rightLedgeRaycast = Physics2D.Raycast(new Vector2(transform.position.x + raycastOffest.x, transform.position.y + raycastOffest.y), Vector2.down, raycastLengthLedge);
        if (rightLedgeRaycast.collider == null) direction = -1;

        leftLedgeRaycast = Physics2D.Raycast(new Vector2(transform.position.x - raycastOffest.x, transform.position.y + raycastOffest.y), Vector2.down, raycastLengthLedge);
        if (leftLedgeRaycast.collider == null) direction = 1;

        Debug.DrawRay(new Vector2(transform.position.x + raycastOffest.x, transform.position.y + raycastOffest.y), Vector2.down * raycastLengthLedge, Color.red);
        Debug.DrawRay(new Vector2(transform.position.x - raycastOffest.x, transform.position.y + raycastOffest.y), Vector2.down * raycastLengthLedge, Color.red);

        //makes enemy switch direction if it hits a wall

        rightWallRaycast = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.right, raycastLengthWall, raycastLayerMask);
        if (rightWallRaycast.collider != null) direction = -1;

        leftWallRaycast = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.left, raycastLengthWall, raycastLayerMask);
        if (leftWallRaycast.collider != null) direction = 1;


        #endregion

        if (direction == -1)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        lineOfSight = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), new Vector2(direction, 0), raycastLengthLineOfSight, playerRaycastLayerMask);
        if (lineOfSight.collider != null)
        {
            spotPlayerAnnouncer.SetActive(true);

            if (Time.time >= timeBetweenShots)
            {
                Shoot();
                Debug.Log("SHOOTING PLAYER");
                shootSound.Play();
                timeBetweenShots = Time.time + 1f / fireRate;
            }
        }

        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y), Vector2.right * raycastLengthWall, Color.blue);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y), Vector2.left * raycastLengthWall, Color.blue);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y), new Vector2(direction, 0) * raycastLengthLineOfSight, Color.magenta);

    }

    public void Shoot()
    {

        GameObject projectile = bulletPool.Get();
        projectile.transform.position = shootingPosition.position;

        if (direction == 1)
        {
            projectile.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        }
        else
        {
            projectile.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 180f));
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            StartCoroutine(player.Die());
        }
    }
}
