using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WallFade : MonoBehaviour
{
    public SpriteRenderer wallRend;


    private void Start()
    {
        wallRend = this.gameObject.GetComponentInParent<SpriteRenderer>();
    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            wallRend.color = new Color(wallRend.color.r, wallRend.color.g, wallRend.color.b, 0.2f);
        }

        /*if (coll.CompareTag("enemyBody"))
        {
            if (wallRend.color.a != 0.2f)
            {
                wallRend.color = new Color32(255, 200, 200, 50);
            }
        }*/
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            wallRend.color = new Color(wallRend.color.r, wallRend.color.g, wallRend.color.b, 1f);
        }

        if (coll.CompareTag("enemyBody"))
        {
            if (wallRend.color.a != 0.2f)
            {
                wallRend.color = new Color32(255, 255, 255, 255);
            }
        }
    }
}
