using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackToMyMenu : MonoBehaviour
{
    public Button menuBtn;

    private void Start()
    {
        menuBtn.onClick.AddListener(BackToMenu);
    }

    void BackToMenu()
    {
        Destroy(GameObject.FindWithTag("Container"));
        SceneManager.LoadScene(0);
    }
}
