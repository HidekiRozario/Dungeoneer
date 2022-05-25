using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour
{
    public int level;
    public int levelMax;



    private void Start()
    {
        level = GameObject.FindWithTag("Container").GetComponent<LevelContainer>().Currentlevel;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player") && level <= levelMax)
        {
            GameObject.FindWithTag("Container").GetComponent<LevelContainer>().health = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().Health;
            level++;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        } 
        else if(level > levelMax)
        {
            SceneManager.LoadScene(3);
        }
    }
}
