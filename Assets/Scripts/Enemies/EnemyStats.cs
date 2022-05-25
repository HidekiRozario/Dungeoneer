using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    [Header("Stats")]
    public float enemyHP;
    public int score;

    [Header("HP")]
    public Image healthBar;
    float startEnemyHP;

    private GameController GM;
    AudioSource enemyAudio;
    

    public GameObject DeathParticle;

    void Start()
    {
        enemyAudio = GetComponent<AudioSource>();
        GM = GameObject.Find("GameController").GetComponent<GameController>();
        startEnemyHP = enemyHP;
    }

    void Update()
    {
        if (enemyHP <= 0)
        {
            /* SEND SCORE TO GAME CONTROLLER */
            GM.AddScore(score);
            die();
            Destroy(transform.parent.gameObject);
        }
    }

    public void Hit(float dmg)
    {
        enemyHP -= dmg;
        healthBar.fillAmount = enemyHP / startEnemyHP;
        enemyAudio.Play();
    }

    public void die()
    {
        Instantiate(DeathParticle);
    }
}
