using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class GameControllerDebug : MonoBehaviour
{
    [Header("Spawnable")]
    public GameObject player;

    [Header("PlayerHP")]
    public Image Heart1;
    public Image Heart2;
    public Image Heart3;

    public Sprite fullHP;
    public Sprite halfHP;
    public Sprite emptyHP;

    PlayerMovement playerScript;

    private void Awake()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
    }

    void Update()
    {
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
    }
}
