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

    [Header("Controls")]
    public bool Playing = false;   // Are we playing?
    public bool Jumping = false;   // Are we jumping?
    public float JumpForce = 1.0f; // Force applied to player when we jump

    [Header("Sound")]
    public AudioSource SoundPlayer; // AudioSource to play BGM and misc sounds
    public AudioClip DeathClip;     // Clip to play when the player dies

    [Header("UI")]
    public GameObject DeathScreen;          // UI to show when the player dies
    public GameObject NewHighScoreMessage;  // Message to display if we get a new highscore
    public Text FinalScoreDisplay;          // Text to display the final score

    private Animator animator;  // Animator for the player
    private Rigidbody2D rb2d;   // Rigidbody2D for the player

    void Awake()
    {
        // Get the Animator component
        animator = GetComponent<Animator>();

        // Get the Rigidbody2D component
        rb2d = GetComponent<Rigidbody2D>();

        // If animator is not null
        if (animator)
        {
            // We are playing
            animator.SetBool("Playing", true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // If animator is not null and we are playing
        if (animator && animator.GetBool("Playing"))
        {
            // Determine if the user is falling
            bool Falling = rb2d.velocity.y < -0.01;

            // Check if the user tapped the screen while on a platform
            if (!Jumping && !Falling && Input.GetMouseButtonDown(0))
            {
                // If they did, apply jump force
                rb2d.velocity = new Vector2(0, JumpForce);

                // Set Jumping variable
                Jumping = true;
            }

            // Set the Vertical Velocity parameter on the animator
            animator.SetFloat("Vertical Velocity", rb2d.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If we hit a collectable
        if (collision.CompareTag("Collectable"))
        {
            // Get the collectable component on the object
            Collectable collectable = collision.GetComponent<Collectable>();

            // Update the score with the value of the collectable
            Score += collectable.Value;

            // Play the sound for collecting that specific collectable
            SoundPlayer.PlayOneShot(collectable.CollectSound);

            // Destroy the collectable
            Destroy(collision.gameObject);

            // Update the current score display
            ScoreDisplay.text = "Score: " + Score.ToString();
        }

        // If we hit the water
        if (collision.CompareTag("Water"))
        {
            // Stop playing the BGM
            SoundPlayer.Stop();

            // If animator is not null
            if (animator)
            {
                // We are not playing
                animator.SetBool("Playing", false);
            }

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If we collide with a platform
        if (collision.gameObject.CompareTag("Platform"))
        {
            // And we hit it from above
            if (collision.gameObject.transform.position.y < transform.position.y)
            {
                // We landed and are no longer jumping
                Jumping = false;

                // Make the platform unstable
                collision.gameObject.GetComponent<PlatformController>().MakeUnstable();
            }
        }
    }
}
