using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpForce = 10f;

    [SerializeField] private float groundCheckRadius = 1f;
    public GameObject checkGround;
    public bool isGrounded;

    public Timer[] timers = new Timer[2];

    [SerializeField] private Transform shootingPosition;
    [SerializeField] private float timeBetweenShots = 1f;
    [SerializeField] private float fireRate = 3f;
    [SerializeField] private GameObject bulletPrefab;
    public ObjectPool<GameObject> bulletPool;

    public Animator playerAnimator;

    public bool canShoot = false;
    public bool isRunning = false;
    public bool facingRight = true;

    //Sounds
    public AudioSource jumpSound;
    public AudioSource shootSound;
    public AudioSource dieSound;

    private Rigidbody2D rb2d;
    private PlayerInputManager input;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        input = GetComponent<PlayerInputManager>();

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

    // Update is called once per frame
    void Update()
    {
        Move(input.NormInputX);

        playerAnimator.SetBool("isRunning", isRunning);
        playerAnimator.SetBool("isGrounded", isGrounded);

        if (input.NormInputX != 0)
        {
            isRunning = true;
            FaceForward(input.NormInputX);
        }
        else
        {
            isRunning = false;
        }

        if (transform.localScale.x == 1)
        {
            facingRight = true;
        }
        else
        {
            facingRight = false;
        }

        #region Jump

        int groundLayerMask = LayerMask.GetMask("Floor");

        isGrounded = Physics2D.OverlapCircle(checkGround.transform.position, groundCheckRadius, groundLayerMask);

        if (input.JumpInput)
        {
            if (isGrounded)
            {
                Jump(jumpForce);
            }
        }

        #endregion

        #region Shooting

        if (input.ShootInput && canShoot)
        {
            if (Time.time >= timeBetweenShots)
            {
                Shoot();
                timeBetweenShots = Time.time + 1f / fireRate;
            }
        }

        #endregion

        #region Timers

        //timer[0] == Moving Timer
        //timer[1] == Stillness Timer
        if (rb2d.velocity.x != 0 || rb2d.velocity.y != 0)
        {
            timers[0].timerIsRunning = true;
            timers[1].timerIsRunning = false;
        }
        else
        {
            timers[0].timerIsRunning = false;
            timers[1].timerIsRunning = true;
        }

        #endregion

        if (timers[0].time == 0 || timers[1].time == 0)
        {
            StartCoroutine(Die());
        }

    }

    public void Move(float horizontal)
    {
        rb2d.velocity = new Vector2(horizontal * moveSpeed, rb2d.velocity.y);
    }

    public void Jump(float jumpPower)
    {
        jumpSound.Play();
        rb2d.velocity = new Vector2(rb2d.velocity.x, jumpPower);
    }

    public void Shoot()
    {
        shootSound.Play();

        GameObject projectile = bulletPool.Get();
        projectile.transform.position = shootingPosition.position;

        if (facingRight)
        {
            projectile.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        }
        else
        {
            projectile.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 180f));
        }
    }

    public IEnumerator Die()
    {
        dieSound.Play();
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
    }

    public void FaceForward(float facingDir)
    {
        gameObject.transform.localScale = new Vector3(facingDir, 1, 1);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(checkGround.transform.position, groundCheckRadius);
    }
}
