using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyhit : MonoBehaviour
{
    SpriteRenderer enemy;

    bool hit;

    float hitStopTime;
    public float hitCooldown;

    Color32 hitCol = new Color32(255, 200, 200, 255);
    Color32 defaultCol = new Color32(255, 255, 255, 255);

    private void Start()
    {
        hit = false;
        enemy = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (hitStopTime <= Time.time)
        {
            hit = false;
        }

        if(hit == true)
        {
            enemy.color = hitCol;
        }
        else
        {
            enemy.color = defaultCol;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            hitStopTime = Time.time + hitCooldown;
            hit = true;
        }
    }
}
