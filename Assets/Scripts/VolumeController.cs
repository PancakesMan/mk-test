using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public Sprite[] ButtonStates;
    public SoundController controller;

    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
        if (button)
        {
            button.image.sprite = ButtonStates[PlayerPrefs.GetInt("Volume", 1)];
        }
    }

    public void ToggleVolume()
    {
        int volume = PlayerPrefs.GetInt("Volume", 1);

        // Toggle volume between the values 0 and 1
        volume = volume == 1 ? 0 : 1;
        PlayerPrefs.SetInt("Volume", volume);

        // If button is not null, update the image
        if (button)
        {
            button.image.sprite = ButtonStates[volume];
        }

        // If controller is not null, call UpdateSound()
        if(controller)
        {
            controller.UpdateSound();
        }
    }
}
