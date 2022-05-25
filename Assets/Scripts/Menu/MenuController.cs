using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [Header("Buttons")]
    public Button playBtn;
    public Button quitBtn;
    public Button settBtn;
    public Button helpBtn;
    public Button settBackBtn;
    public Button helpBackBtn;
    public Toggle fullscreenTogg;
    public Slider musicSlider;

    public Animator animSett;
    public Animator animHelp;

    private void Start()
    {
        musicSlider.value = AudioListener.volume;
        playBtn.onClick.AddListener(Play);
        quitBtn.onClick.AddListener(Quit);
        settBtn.onClick.AddListener(SettingsOpen);
        helpBtn.onClick.AddListener(HelpOpen);
        settBackBtn.onClick.AddListener(SettingsClose);
        helpBackBtn.onClick.AddListener(HelpClose);
    }

    private void Update()
    {
        if (fullscreenTogg.isOn)
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        } else if (!fullscreenTogg.isOn)
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }

        AudioListener.volume = musicSlider.value;
    }

    private void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void Quit()
    {
        Application.Quit();
    }

    private void SettingsOpen()
    {
        animSett.SetBool("isOpened", true);
    }
    private void SettingsClose()
    {
        animSett.SetBool("isOpened", false);
    }

    private void HelpOpen()
    {
        animHelp.SetBool("open", true);
    }
    private void HelpClose()
    {
        animHelp.SetBool("open", false);
    }
}
