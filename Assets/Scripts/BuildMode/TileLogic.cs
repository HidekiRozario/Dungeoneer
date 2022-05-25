using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileLogic : MonoBehaviour, IDropHandler
{
    public Vector2Int GridPos;
    public GameObject Slot;

    public bool LevelsOnly;
    public bool SpecialOnly;

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            if (transform.childCount > 0)
                return;
            Slot = eventData.pointerDrag;

            if((!LevelsOnly && !SpecialOnly) || (SpecialOnly && Slot.GetComponent<TileDisplay>().levelTile.isLevel == false) || (LevelsOnly && Slot.GetComponent<TileDisplay>().levelTile.isLevel))
            {
                GetComponent<AudioSource>().Play();
                Slot.GetComponent<DragBehaviou>().snaped = true;
                Slot.transform.parent = transform;
                transform.GetChild(0).transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
            }

        }
    }

}
