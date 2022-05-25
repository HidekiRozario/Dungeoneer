using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //Code by Brackeys thank you <3
    [Header("Player")]
    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    private void Start()
    {
        target = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, -10), desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
