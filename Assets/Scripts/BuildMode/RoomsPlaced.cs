using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomsPlaced : MonoBehaviour
{
    public Text roomsTXT;

    int rooms;

    private void Update()
    {
        rooms = GameObject.Find("Map Renderer").GetComponent<RenderMap>().resultOfLevels;

        roomsTXT.text = rooms.ToString() + "/6";

        if(rooms != 6)
        {
            roomsTXT.color = new Color32(255, 200, 200, 255);
        } else
        {
            roomsTXT.color = new Color32(0, 0, 0, 255);
        }
    }
}
