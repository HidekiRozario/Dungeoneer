using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEnemy : MonoBehaviour
{
    public float enemySpeed = 2f;
    public float chasingDistance = 7f;

    [Header("Components")]
    public Rigidbody2D enemyRB;
    public BoxCollider2D enemyHitBox;

    [Header("Cooldowns")]
    public float atkCooldown;
    float timeAtk;

    Transform player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        enemyRB = gameObject.GetComponent<Rigidbody2D>();
        enemyHitBox = this.gameObject.GetComponentInChildren<BoxCollider2D>();
    }

    private void Update()
    {
        if (timeAtk <= Time.time)
        {
            enemyHitBox.enabled = true;
        }
        if (Vector2.Distance(transform.position, player.position) < chasingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, enemySpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        foreach (ContactPoint2D contact in coll.contacts)
        {
            var colName = contact.otherCollider.name;
            if(colName == "EnemyBody")
            {
                if (coll.collider.CompareTag("Player"))
                {
                    timeAtk = atkCooldown + Time.time;
                    enemyHitBox.enabled = false;
                }
            }
        }
    }
}
