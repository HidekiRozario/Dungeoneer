using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    public SpriteRenderer playerSprite;
    public Rigidbody2D playerRB;
    public CapsuleCollider2D playerHitBox;
    public GameObject bullet;
    public Animator playerAnim;
    public AudioSource playerAudio;

    [Header("Stats")]
    public float Health = 3;
    public float moveSpeed;

    [Header("Speed")]
    public float bulletSpeed;
    public float rollSpeed;
    public float shotCooldown;

    [Header("Cooldowns")]
    public float rollStopTime;
    public float rollCooldown;

    float directionY;
    float directionX;
    float y;
    float x;
    float timeShot;
    float timeRolling;
    float timeRoll;
    bool canShoot = true;
    bool canRoll = true;
    bool isRolling = false;

    private void Start()
    {
        Health = GameObject.FindWithTag("Container").GetComponent<LevelContainer>().health;
        playerSprite = GetComponent<SpriteRenderer>();
        playerAnim = GetComponent<Animator>();
        playerRB = GetComponent<Rigidbody2D>();
        playerHitBox = GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.LeftShift) && canRoll && playerRB.velocity != Vector2.zero))
        {
            directionY = Input.GetAxis("Vertical");
            directionX = Input.GetAxis("Horizontal");
            timeRolling = Time.time + rollStopTime;
            timeRoll = Time.time + rollCooldown;
            canRoll = false;
            isRolling = true;

            playerSprite.color = new Color32(255, 255, 255, 100);
        }
    }

    void FixedUpdate()
    {
        //----------------MOVING------------------------

        y = Input.GetAxis("Vertical");
        x = Input.GetAxis("Horizontal");
        if (!isRolling)
        {
            Move();
        }

        //-----------------------------------------------
        //----------------SHOOTING-----------------------

        float shootX = Input.GetAxisRaw("HorizontalShoot");
        float shootY = Input.GetAxisRaw("VerticalShoot");

        if ((shootX != 0 || shootY != 0) && canShoot)
        {
            
            Shoot(shootX, shootY);
            canShoot = false;
            timeShot = Time.time + shotCooldown;
        }

        if (timeShot <= Time.time)
        {
            canShoot = true;
        }

        //------------------------------------------------
        //-----------------ROLL---------------------------
        
        if (timeRolling <= Time.time)
        {
            isRolling = false;
            playerSprite.color = new Color32(255, 255, 255, 255);
            playerHitBox.enabled = true;
        }

        if (timeRoll <= Time.time)
        {
            canRoll = true;
            isRolling = false;
        }

        if (isRolling)
        {
            canShoot = false;
            playerHitBox.enabled = false;
            float rollingSpeed = rollSpeed * Time.deltaTime;
            playerRB.velocity = new Vector2(directionX, directionY) * rollingSpeed;
        }

        //------------------------------------------------
    }

    void Shoot(float directionX, float directionY)
    {
        GameObject bulletPrefab = Instantiate(bullet, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, -7f), Quaternion.identity);
        Rigidbody2D bulletRB = bulletPrefab.GetComponent<Rigidbody2D>();
        bulletRB.AddForce(new Vector2(directionX, directionY) * bulletSpeed, ForceMode2D.Impulse);
    }

    void Move()
    {
        float moveByY = y * moveSpeed * Time.deltaTime;
        float moveByX = x * moveSpeed * Time.deltaTime;
        playerRB.velocity = new Vector3(moveByX, moveByY, 0);
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.CompareTag("enemyBody"))
        {
            Health -= 0.5f;
            playerAudio.Play();
        }
        if (coll.collider.CompareTag("enemyAtk"))
        {
            Health -= 0.5f;
            playerAudio.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("enemyAtk"))
        {
            Health -= 0.5f;
            playerAudio.Play();
        }
    }
}
