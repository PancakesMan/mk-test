using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class SoundController : MonoBehaviour
{
    [Header("Volume Button")]
    public Button button;
    public Sprite[] ButtonStates;

    [Header("BGM Info")]
    public AudioClip[] BGMList;
    public Text BGMDisplay;

    private AudioSource source;

    void Awake()
    {
        if (button)
        {
            button.image.sprite = ButtonStates[PlayerPrefs.GetInt("Volume", 1)];
        }

        source = GetComponent<AudioSource>();
        UpdateSound();
    }

    public void Next()
    {
        int current = PlayerPrefs.GetInt("BGM Track", 0);
        current = (current + 1) >= BGMList.Length ? 0 : current + 1;
        PlayerPrefs.SetInt("BGM Track", current);

        UpdateSound();
    }

    public void Previous()
    {
        int current = PlayerPrefs.GetInt("BGM Track", 0);
        current = (current - 1) < 0 ? BGMList.Length - 1 : current - 1;
        PlayerPrefs.SetInt("BGM Track", current);

        UpdateSound();
    }

    public void UpdateSound()
    {
        source.Stop();
        source.clip = BGMList[PlayerPrefs.GetInt("BGM Track", 0)];
        source.volume = PlayerPrefs.GetInt("Volume", 1);
        source.Play();

        if (BGMDisplay)
        {
            BGMDisplay.text = source.clip.name;
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

        UpdateSound();
    }
}
