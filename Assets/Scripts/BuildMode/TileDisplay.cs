using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileDisplay : MonoBehaviour
{
    public LevelTile levelTile;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        GetComponent<SpriteRenderer>().sprite = levelTile.icon;
        if (levelTile.isLevel)
        {
            GetComponentInChildren<Text>().text = levelTile.LevelDifficulty.ToString();

        }
        else
        {
            GetComponentInChildren<Text>().text = "";
            GetComponentInChildren<Image>().enabled = false;
        }
    }
}
