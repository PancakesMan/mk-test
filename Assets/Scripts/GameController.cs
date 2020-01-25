﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Score")]
    [Header("Game Settings")]
    public int Score = 0;      // Current Score
    public Text ScoreDisplay;  // Display for the current score

    [Header("Player")]
    public PlayerController Player; // Reference to the player

    [Header("Sound")]
    public AudioSource SoundPlayer; // AudioSource to play BGM and misc sounds
    public AudioClip DeathClip;     // Clip to play when the player dies

    [Header("UI")]
    public GameObject DeathScreen;          // UI to show when the player dies
    public Text DeathReason;                // Text to display how you died
    public GameObject NewHighScoreMessage;  // Message to display if we get a new highscore
    public Text FinalScoreDisplay;          // Text to display the final score

    void Awake()
    {
        // If Player is not null
        if (Player)
        {
            // Subscribe to the Player's OnDeath event
            Player.OnDeath.AddListener(GameOver);

            // Subscribe to the Player's OnPickupCollected event
            Player.OnPickupCollectable.AddListener(ItemCollected);
        }
    }

    private void ItemCollected(Collectable item)
    {
        // Update the score with the value of the collectable
        Score += item.Value;

        // Play the sound for collecting that specific collectable
        SoundPlayer.PlayOneShot(item.CollectSound);

        // Disable the collectable
        item.gameObject.SetActive(false);

        // Update the current score display
        ScoreDisplay.text = "Score: " + Score.ToString();
    }

    private void GameOver(string reason = "")
    {
        // Stop playing the BGM
        SoundPlayer.Stop();

        // Play the death clip
        SoundPlayer.PlayOneShot(DeathClip);

        // Stop showing the current score
        ScoreDisplay.gameObject.SetActive(false);

        // Activate the death screen
        DeathScreen.SetActive(true);

        // Show the reason we died
        DeathReason.text = reason;

        // Show the final score we had when we died
        FinalScoreDisplay.text = "Score: " + Score.ToString();

        // Get the current highscore
        int Highscore = PlayerPrefs.GetInt("Highscore", 0);

        // If we beat the highscore
        if (Score > Highscore)
        {
            // Show the new highscore message
            NewHighScoreMessage.SetActive(true);

            // Update the highscore
            PlayerPrefs.SetInt("Highscore", Score);
        }
    }
}
