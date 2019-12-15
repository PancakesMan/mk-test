using System.Collections;
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
    public GameObject NewHighScoreMessage;  // Message to display if we get a new highscore
    public Text FinalScoreDisplay;          // Text to display the final score

    void Awake()
    {
        if (Player)
        {
            Player.OnDeath.AddListener(GameOver);
            Player.OnPickupCollectable.AddListener(ItemCollected);
        }
    }

    private void ItemCollected(Collectable item)
    {
        // Update the score with the value of the collectable
        Score += item.Value;

        // Play the sound for collecting that specific collectable
        SoundPlayer.PlayOneShot(item.CollectSound);

        // Destroy the collectable
        Destroy(item.gameObject);

        // Update the current score display
        ScoreDisplay.text = "Score: " + Score.ToString();
    }

    private void GameOver()
    {
        // Stop playing the BGM
        SoundPlayer.Stop();

        // Play the death clip
        SoundPlayer.PlayOneShot(DeathClip);

        // Stop showing the current score
        ScoreDisplay.gameObject.SetActive(false);

        // Activate the death screen
        DeathScreen.SetActive(true);

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
