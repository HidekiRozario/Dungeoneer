using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpEnemy : MonoBehaviour
{
    public float enemySpeed;
    public float jumpCooldown;
    public bool canJump;

    public float detectionRadius;

    Transform playerTrans;

    public Vector2 TargetPoint;

    [Header("Components")]
    public Rigidbody2D enemyRB;

    private void Start()
    {
        playerTrans = GameObject.FindWithTag("Player").transform;
        enemyRB = gameObject.GetComponent<Rigidbody2D>();
        InvokeRepeating("setTrue", jumpCooldown, jumpCooldown);

        TargetPoint = transform.position;
    }

    void setTrue()
    {
        canJump = true;
    }



    private void Update()
    {

        if (canJump && Vector2.Distance(transform.position, playerTrans.position) < detectionRadius)
        {
            TargetPoint = playerTrans.position;
            canJump = false;
        }

        enemyRB.transform.position = Vector2.MoveTowards(enemyRB.transform.position, TargetPoint, enemySpeed * Time.deltaTime);

    }

}


