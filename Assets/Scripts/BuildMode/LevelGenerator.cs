using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelGenerator : MonoBehaviour
{
    private LevelContainer levelContainer;

    public GameObject ContainerPrefab;
    public LevelTile[] allLevels;


    void Start()
    {
        try
        {
            levelContainer = GameObject.FindWithTag("Container").GetComponent<LevelContainer>();
        }
        catch (Exception)
        {
            levelContainer = Instantiate(ContainerPrefab, gameObject.transform.position, Quaternion.identity).GetComponent<LevelContainer>();
        }

        if (levelContainer.Currentlevel > 4)
            return;
        
        for(int i = 0; i < transform.childCount - 2; i++)//-2 because we dont want to change start and end
        {
            int random;
            do
            {
                random = UnityEngine.Random.Range(0, allLevels.Length);

            } while (!fitsCurrent(levelContainer.Currentlevel ,allLevels[random].LevelDifficulty));
            
            transform.Find("Level Holder (" + i + ")").GetChild(0).GetComponent<TileDisplay>().levelTile = allLevels[random];
        }
    }

    bool fitsCurrent(int level, int difficulty)
    {
        switch (level)
        {
            case 1:
                if (difficulty > 0 && difficulty < 5)
                    return true;
                break;
            case 2:
                if (difficulty > 1 && difficulty < 6)
                    return true;
                break;
            case 3:
                if (difficulty > 2 && difficulty < 7)
                    return true;
                break;
            case 4:
                if (difficulty > 3 && difficulty < 8)
                    return true;
                break;
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
