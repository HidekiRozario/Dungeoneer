using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentLevel : MonoBehaviour
{
    public Text currentLvl;

    private void Update()
    {
        currentLvl.text = GameObject.FindWithTag("Container").GetComponent<LevelContainer>().Currentlevel.ToString();
    }
}
