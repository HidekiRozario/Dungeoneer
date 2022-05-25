using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Controller")]
    public int level;
    public int score;

    public AudioClip music1;
    public AudioClip music2;
    public AudioClip music3;

    public AudioSource musicPlayer;

    [Header("Spawnable")]
    public GameObject player;

    [Header("Player")]
    public Image Heart1;
    public Image Heart2;
    public Image Heart3;

    public Sprite fullHP;
    public Sprite halfHP;
    public Sprite emptyHP;

    private GameObject container;

    Text scoreTXT;

    [Header("Level")]
    PlayerMovement playerScript;
    LevelTile[,] levelArray;
    GameObject[,] roomArray = new GameObject[8, 5];
    public float levelOffset;
    public GameObject openDoorsHorizontal;
    public GameObject closedDoorsHorizontal;
    public GameObject openDoorsVertical;
    public GameObject closedDoorsVertical;

    private void Awake()
    {
        //Music Setup
        musicPlayer = GetComponent<AudioSource>();
        musicPlayer.clip = music1;
        musicPlayer.Play();

        //Level Building
        container = GameObject.FindWithTag("Container");
        levelArray = container.GetComponent<LevelContainer>().LevelArray;
        level = container.GetComponent<LevelContainer>().Currentlevel;
        score = container.GetComponent<LevelContainer>().score;
        BuildLevel();

        //Level Setting
        playerScript = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        scoreTXT = GameObject.Find("ScoreText").GetComponent<Text>();
        container.GetComponent<LevelContainer>().Currentlevel++;
    }

    public void AddScore(int scoreAdd)
    {
        score += scoreAdd;
    }

    void BuildLevel()
    {
        for(int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 5; j++)
            {
                
                if(levelArray[i, j] != null)
                {
                    Vector2 roomPos = new Vector2(i * 40f - levelOffset, j * 25f - levelOffset);
                    roomArray[i, j] = Instantiate(levelArray[i, j].mapPrefab, roomPos, Quaternion.identity);
                }
                else
                {
                    roomArray[i, j] = null;
                }
            }
        }

        for(int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if(roomArray[i, j] != null)
                {
                    if(i == 7)
                    {
                        Instantiate(closedDoorsHorizontal, new Vector3(roomArray[i, j].transform.position.x + 24.5f, roomArray[i, j].transform.position.y - 3f, -8f), Quaternion.identity);
                    }
                    if (i == 0)
                    {
                        Instantiate(closedDoorsHorizontal, new Vector3(roomArray[i, j].transform.position.x - 15.46f, roomArray[i, j].transform.position.y - 3f, -8f), Quaternion.identity);
                    }
                    if (j == 4)
                    {
                        Instantiate(closedDoorsVertical, new Vector3(roomArray[i, j].transform.position.x + 4.5f, roomArray[i, j].transform.position.y + 6.83f), Quaternion.identity);
                    }
                    if (j == 0)
                    {
                        Instantiate(closedDoorsVertical, new Vector3(roomArray[i, j].transform.position.x + 4.5f, roomArray[i, j].transform.position.y - 18.12f), Quaternion.identity);
                    }
                    if (i < 7 && j < 4)
                    {
                        if (roomArray[i + 1, j] != null)
                        {
                            Instantiate(openDoorsHorizontal, new Vector3(roomArray[i, j].transform.position.x + 24.5f, roomArray[i, j].transform.position.y - 3f, -8f), Quaternion.identity);
                        }
                        else
                        {
                            Instantiate(closedDoorsHorizontal, new Vector3(roomArray[i, j].transform.position.x + 24.5f, roomArray[i, j].transform.position.y - 3f, -8f), Quaternion.identity);
                        }
                        if (roomArray[i, j + 1] != null)
                        {
                            Instantiate(openDoorsVertical, new Vector3(roomArray[i, j].transform.position.x + 4.5f, roomArray[i, j].transform.position.y + 6.83f), Quaternion.identity);
                        }
                        else
                        {
                            Instantiate(closedDoorsVertical, new Vector3(roomArray[i, j].transform.position.x + 4.5f, roomArray[i, j].transform.position.y + 6.83f), Quaternion.identity);
                        }
                    }

                    if (j > 0)
                    {
                        if (roomArray[i, j - 1] != null)
                        {
                            Instantiate(openDoorsVertical, new Vector3(roomArray[i, j].transform.position.x + 4.5f, roomArray[i, j].transform.position.y - 18.12f), Quaternion.identity);
                        }
                        else
                        {
                            Instantiate(closedDoorsVertical, new Vector3(roomArray[i, j].transform.position.x + 4.5f, roomArray[i, j].transform.position.y - 18.12f), Quaternion.identity);
                        }

                        if (j < 4 && roomArray[i, j + 1] != null && j < 4)
                        {
                            Instantiate(openDoorsVertical, new Vector3(roomArray[i, j].transform.position.x + 4.5f, roomArray[i, j].transform.position.y + 6.83f), Quaternion.identity);
                        }
                        else
                        {
                            Instantiate(closedDoorsVertical, new Vector3(roomArray[i, j].transform.position.x + 4.5f, roomArray[i, j].transform.position.y + 6.83f), Quaternion.identity);
                        }
                    }

                    if (i > 0)
                    {
                        if (levelArray[i - 1, j] != null)
                        {
                            Instantiate(openDoorsHorizontal, new Vector3(roomArray[i, j].transform.position.x - 15.46f, roomArray[i, j].transform.position.y - 3f, -8f), Quaternion.identity);
                        }
                        else
                        {
                            Instantiate(closedDoorsHorizontal, new Vector3(roomArray[i, j].transform.position.x - 15.46f, roomArray[i, j].transform.position.y - 3f, -8f), Quaternion.identity);
                        }

                        if (i < 7 && levelArray[i + 1, j] != null)
                        {
                            Instantiate(openDoorsHorizontal, new Vector3(roomArray[i, j].transform.position.x + 24.5f, roomArray[i, j].transform.position.y - 3f, -8f), Quaternion.identity);
                        }
                        else
                        {
                            Instantiate(closedDoorsHorizontal, new Vector3(roomArray[i, j].transform.position.x + 24.5f, roomArray[i, j].transform.position.y - 3f, -8f), Quaternion.identity);
                        }
                    }
                }
            }
        }
    }

    void Update()
    {
        scoreTXT.text = score.ToString();
        container.GetComponent<LevelContainer>().score = score;

        //MUSIC
        if(musicPlayer.clip == music1 && musicPlayer.time >= music1.length)
        {
            musicPlayer.clip = music2;
            musicPlayer.Play();
        } 
        else if(musicPlayer.clip == music2 && musicPlayer.time >= music2.length)
        {
            musicPlayer.clip = music3;
            musicPlayer.Play();
        }
        else if (musicPlayer.clip == music3 && musicPlayer.time >= music3.length)
        {
            musicPlayer.clip = music1;
            musicPlayer.Play();
        }

        //PLAYER UI
        switch (playerScript.Health)
        {
            case 3f:
                Heart1.sprite = fullHP;
                Heart2.sprite = fullHP;
                Heart3.sprite = fullHP;
                break;
            case 2.5f:
                Heart1.sprite = fullHP;
                Heart2.sprite = fullHP;
                Heart3.sprite = halfHP;
                break;
            case 2f:
                Heart1.sprite = fullHP;
                Heart2.sprite = fullHP;
                Heart3.sprite = emptyHP;
                break;
            case 1.5f:
                Heart1.sprite = fullHP;
                Heart2.sprite = halfHP;
                Heart3.sprite = emptyHP;
                break;
            case 1f:
                Heart1.sprite = fullHP;
                Heart2.sprite = emptyHP;
                Heart3.sprite = emptyHP;
                break;
            case 0.5f:
                Heart1.sprite = halfHP;
                Heart2.sprite = emptyHP;
                Heart3.sprite = emptyHP;
                break;
            case 0f:
                Heart1.sprite = emptyHP;
                Heart2.sprite = emptyHP;
                Heart3.sprite = emptyHP;
                break;
            default:
                Heart1.sprite = emptyHP;
                Heart2.sprite = emptyHP;
                Heart3.sprite = emptyHP;
                break;
        }

        //Player Death

        if (playerScript.Health <= 0)
        {
            container.GetComponent<LevelContainer>().score = score;
            SceneManager.LoadScene(3);
        }
    }
}
