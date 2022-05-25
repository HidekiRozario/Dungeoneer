using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Level Tile", menuName ="Level Tile")]
public class LevelTile : ScriptableObject
{
    public Sprite icon;
    public bool isLevel;
    public string LevelName;
    public int LevelDifficulty;
    public GameObject mapPrefab;

}
