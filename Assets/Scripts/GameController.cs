using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Score")]
    [Header("Game Settings")]
    public int Score = 0;
    public Text ScoreDisplay;

    [Header("Controls")]
    public bool Playing = false;
    public bool Jumping = false;
    public float JumpForce = 1.0f;

    [Header("Sound")]
    public AudioSource SoundPlayer;
    public AudioClip DeathClip;

    [Header("UI")]
    public GameObject DeathScreen;
    public GameObject NewHighScoreMessage;
    public Text FinalScoreDisplay;

    private Animator animator;
    private Rigidbody2D rb2d;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();

        //debug
        animator.SetBool("Playing", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetBool("Playing"))
        {
            // User tapped the screen
            if (!Jumping && Input.GetMouseButtonDown(0))
            {
                rb2d.velocity = new Vector2(0, JumpForce);
                Jumping = true;
            }

            animator.SetFloat("Vertical Velocity", rb2d.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Collectable"))
        {
            Collectable collectable = collision.GetComponent<Collectable>();
            Score += collectable.Value;
            SoundPlayer.PlayOneShot(collectable.CollectSound);
            Destroy(collision.gameObject);

            ScoreDisplay.text = "Score: " + Score.ToString();
        }

        if (collision.CompareTag("Water"))
        {
            SoundPlayer.Stop();
            animator.SetBool("Playing", false);

            //Death
            SoundPlayer.PlayOneShot(DeathClip);
            ScoreDisplay.gameObject.SetActive(false);

            DeathScreen.SetActive(true);
            FinalScoreDisplay.text = "Score: " + Score.ToString();

            int Highscore = PlayerPrefs.GetInt("Highscore", 0);
            if (Score > Highscore)
            {
                NewHighScoreMessage.SetActive(true);
                PlayerPrefs.SetInt("Highscore", Score);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            Jumping = false;
            collision.gameObject.GetComponent<PlatformController>().MakeUnstable();
        }
    }
}
