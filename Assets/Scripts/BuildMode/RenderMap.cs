using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RenderMap : MonoBehaviour
{
    public GridManager gridManager;
    public Button startButt;

    public LevelTile[,] LevelArray;
    public bool isValid = true;

    public int resultOfLevels;

    private bool[,] wasChecked;

    private GameObject container;

    public GameObject errorMessage;

    private int numberOfLevelsGlobal;


    void Start()
    {
        startButt.onClick.AddListener(OnClickStartButt);
    }

    void Update()
    {
        LevelArray = new LevelTile[gridManager.columns, gridManager.rows];

        for (int x = 0; x < gridManager.columns; x++)
        {
            for (int y = 0; y < gridManager.rows; y++)
            {
                try
                {
                    LevelArray[x, y] = gridManager.grid[x, y].transform.GetChild(0).GetComponent<TileDisplay>().levelTile;
                }
                catch (UnityException)
                {
                    LevelArray[x, y] = null;
                }
            }
        }


        resultOfLevels = 0;
        for (int x = 0; x < gridManager.columns; x++)
        {
            for (int y = 0; y < gridManager.rows; y++)
            {
                if (LevelArray[x, y] == null)
                    continue;
                if (LevelArray[x, y].isLevel)
                {
                    resultOfLevels++;
                }

            }

        }
        numberOfLevelsGlobal = resultOfLevels;



    }

    void OnClickStartButt()
    {

        LevelArray = new LevelTile[gridManager.columns, gridManager.rows];

        for (int x = 0; x < gridManager.columns; x++)
        {
            for (int y = 0; y < gridManager.rows; y++)
            {
                try
                {
                    LevelArray[x, y] = gridManager.grid[x, y].transform.GetChild(0).GetComponent<TileDisplay>().levelTile;
                }
                catch (UnityException)
                {
                    LevelArray[x, y] = null;
                }
            }
        }



        wasChecked = new bool[gridManager.columns, gridManager.rows];
        isValid = true;

        int numberOfLevels = 0;

        Vector2Int start = new Vector2Int(-1, -1);
        LevelTile end = new LevelTile();
        end.LevelName = "\0";


        for (int x = 0; x < gridManager.columns; x++)
        {
            for (int y = 0; y < gridManager.rows; y++)
            {
                if (LevelArray[x, y] == null)
                    continue;
                if(!LevelArray[x, y].isLevel)
                {
                    if (LevelArray[x, y].LevelName == "Start")
                    {
                        start = new Vector2Int(x, y);
                    }
                    else if (LevelArray[x, y].LevelName == "End")
                        end = LevelArray[x, y];

                }
                else
                {
                    numberOfLevels++;
                }
                
                if (x + 1 >= gridManager.columns || LevelArray[x + 1, y] == null)
                {
                    if (x - 1 < 0 || LevelArray[x - 1, y] == null)
                    {
                        if (y + 1 >= gridManager.rows || LevelArray[x, y + 1] == null)
                        {
                            if (y - 1 < 0 || LevelArray[x, y - 1] == null) {
                                isValid = false;
                            }

                        }

                    }

                }

            }

        }

        if (end.LevelName == "\0" || start.x == -1)
        {
            showErrorMessage("Must Contain 1 end block and 1 start block");
            return;
        }else if(numberOfLevels != 6)
        {
            showErrorMessage("Can Contain exactly 6 level blocks");
            return;
        }

        if (isValid)
            isValid = checkAround(start.x, start.y, end);


        if (isValid)
            startGame();
        else
            showErrorMessage("Level is not valid: all blocks must be connected");
    }


    void showErrorMessage(string msg)
    {
        errorMessage.transform.GetChild(0).GetComponent<Text>().text = msg;
        GameObject go = Instantiate(errorMessage);
        go.transform.parent = GameObject.Find("Main Canvas").transform;
        go.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0) - go.transform.localScale / 2.0f;
    }

    void startGame()
    {
        container = GameObject.FindWithTag("Container");
        container.GetComponent<LevelContainer>().LevelArray = LevelArray;
        DontDestroyOnLoad(container);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    bool checkAround(int corX, int corY, LevelTile end)
    {
        if (LevelArray[corX, corY] == end && !wasChecked[corX, corY])
            return true;
        else
            wasChecked[corX, corY] = true;

        if (corX + 1 < gridManager.columns && LevelArray[corX + 1, corY] != null)
            if (!wasChecked[corX + 1, corY] && checkAround(corX + 1, corY, end))
                return true;

        if (corX - 1 >= 0 && LevelArray[corX - 1, corY] != null)
            if (!wasChecked[corX - 1, corY] && checkAround(corX - 1, corY, end))
                return true;

        if (corY + 1 < gridManager.rows && LevelArray[corX, corY + 1] != null)
            if (!wasChecked[corX, corY + 1] && checkAround(corX, corY + 1, end))
                return true;

        if (corY - 1 >= 0 && LevelArray[corX, corY - 1] != null)
            if (!wasChecked[corX, corY - 1] && checkAround(corX, corY - 1, end))
                return true;

        return false;
    }



}
