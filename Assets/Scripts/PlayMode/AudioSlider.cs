using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    public Slider musicSlider;

    private void Start()
    {
        musicSlider.value = AudioListener.volume;
    }

    private void Update()
    {
        AudioListener.volume = musicSlider.value;
    }
}
