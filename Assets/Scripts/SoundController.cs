using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class SoundController : MonoBehaviour
{
    [Header("Volume Button")]
    public Button button;          // Button to control volume
    public Sprite[] ButtonStates;  // Sprites for the on/off states

    [Header("BGM Info")]
    public AudioClip[] BGMList;    // List of AudioClips available as BGM music
    public Text BGMDisplay;        // Text displaying name of current BGM

    private AudioSource source;

    void Awake()
    {
        // If button is not null
        if (button)
        {
            // Set the button sprite based on the volume state
            button.image.sprite = ButtonStates[PlayerPrefs.GetInt("Volume", 1)];
        }

        // Find the AudioSource component
        source = GetComponent<AudioSource>();

        // Update the AudioSource
        UpdateSound();
    }

    // Select next BGM from list
    public void Next()
    {
        // Get the current BGM track
        int current = PlayerPrefs.GetInt("BGM Track", 0);

        // Go to next index and loop around if we reach the end
        current = (current + 1) >= BGMList.Length ? 0 : current + 1;

        // Set the current BGM track
        PlayerPrefs.SetInt("BGM Track", current);

        // Update the AudioSource
        UpdateSound();
    }

    // Select previous BGM from list
    public void Previous()
    {
        // Get the current BGM track
        int current = PlayerPrefs.GetInt("BGM Track", 0);

        // Go to previous index and loop around if we reach the beginning
        current = (current - 1) < 0 ? BGMList.Length - 1 : current - 1;

        // Set the current BGM track
        PlayerPrefs.SetInt("BGM Track", current);

        // Update the AudioSource
        UpdateSound();
    }

    // Update the AudioSource
    public void UpdateSound()
    {
        // Stop the music
        source.Stop();

        // Update the BGM played by the AudioSource
        source.clip = BGMList[PlayerPrefs.GetInt("BGM Track", 0)];

        // Update the volume state of the AudioSource
        source.volume = PlayerPrefs.GetInt("Volume", 1);

        // Play the BGM with the current volume
        source.Play();

        // if BGM display is not null
        if (BGMDisplay)
        {
            // Set the text to the name of the current BGM
            BGMDisplay.text = source.clip.name;
        }
    }

    public void ToggleVolume()
    {
        // Get the volume state
        int volume = PlayerPrefs.GetInt("Volume", 1);

        // Toggle volume between the values 0 and 1
        volume = volume == 1 ? 0 : 1;

        // Update the volume state
        PlayerPrefs.SetInt("Volume", volume);

        // If button is not null
        if (button)
        {
            // Set the button sprite based on the volume state
            button.image.sprite = ButtonStates[volume];
        }

        // Update the AudioSource
        UpdateSound();
    }
}
