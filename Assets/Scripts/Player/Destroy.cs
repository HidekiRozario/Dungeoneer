using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public float Damage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemyBody"))
        {
            EnemyStats script = collision.GetComponent<EnemyStats>();
            script.Hit(Damage);
            Destroy(this.gameObject);
        }
    }

    void FixedUpdate()
    {
        InvokeRepeating("DestroyObject", 0.5f, 0.5f);
    }

    private void DestroyObject()
    {
        Destroy(this.gameObject);
    }
}
