using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimmedBomb : MonoBehaviour
{
    public float delay = 2.0f;
    void Start()
    {
        Invoke("delete", delay);
    }

    void delete()
    {
        Destroy(gameObject);
    }
}
