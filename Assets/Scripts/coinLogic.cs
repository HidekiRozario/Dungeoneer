using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinLogic : MonoBehaviour
{
    private GameController GM;
    public GameObject DeathParticle;
    public AudioClip coinSound;
    public int score;


    void Start()
    {
        GM = GameObject.Find("GameController").GetComponent<GameController>();
        DeathParticle.GetComponent<AudioSource>().clip = coinSound;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GM.AddScore(score);
            Instantiate(DeathParticle);
            Destroy(gameObject);
        }
    }
}
