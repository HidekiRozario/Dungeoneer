using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragBehaviou : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    private Transform LocalTrans;
    private Vector3 oldPos;

    public bool snaped = false;
    private AudioSource audio;

    private void Awake()
    {
        LocalTrans = GetComponent<Transform>();
        audio = GetComponent<AudioSource>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //make sound begin drag
        audio.Play();
        oldPos = transform.position;
        snaped = false;

        GetComponent<BoxCollider2D>().enabled = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        LocalTrans.position += new Vector3(eventData.delta.x/(Screen.height/10), eventData.delta.y/(Screen.height/10), 0);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(!snaped)
            transform.position = oldPos;

        GetComponent<BoxCollider2D>().enabled = true;
    }


}
