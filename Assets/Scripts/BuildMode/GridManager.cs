using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject[,] grid;

    public GameObject Tile;
    
    public float vertical, horizontal;
    public int columns = 8, rows = 5;

    void Start()
    {
        horizontal = 1.0f / columns;
        vertical = 1.0f / rows;
        grid = new GameObject[columns, rows];



        for(int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                Tile.transform.localScale = new Vector3(horizontal, vertical, 1);
                Tile.transform.position = new Vector3(horizontal * x - .5f, vertical * y - .5f, -1) + Tile.transform.localScale/2.0f;
                grid[x, y] = Instantiate(Tile, transform);
                grid[x, y].GetComponent<TileLogic>().GridPos = new Vector2Int(x, y);
            }
        }
    }

    void Update()
    {
        
    }
}
