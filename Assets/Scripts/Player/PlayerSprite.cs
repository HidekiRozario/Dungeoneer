using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerSprite : MonoBehaviour
{
    float y;
    float x;

    Animator playerAnim;

    private void Start()
    {
        playerAnim = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        playerAnim.SetFloat("Horizontal", x);
        playerAnim.SetFloat("Vertical", y);
    }
}
