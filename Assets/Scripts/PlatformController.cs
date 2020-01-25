using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlatformController : MonoBehaviour
{
    // Event class for when object is disabled
    [System.Serializable]
    public class ObjectDisabledEvent : UnityEvent<GameObject> { };

    public float MaxSpeed = 1.0f;  // Speed at which the platform moves
    public float FallTimer = 1.0f; // How long it takes to fall after BecomeUnstable is called
    private bool Falling = false;  // True if BecomeUnstable was called

    public Transform EndPosition;  // Point on the right-hand edge of the platform

    public ObjectDisabledEvent OnDisabled; // Event fired when object is disabled

    private Rigidbody2D rb2d;      // Rigidbody2D of the platform
    private CoinSpawner coinSpawner;   // CoinSpawner for the platform

    void Awake()
    {
        // Get the Rigidbody2D component
        rb2d = GetComponent<Rigidbody2D>();

        // Get the CoinSpawner component
        coinSpawner = GetComponent<CoinSpawner>();
    }

    private void Update()
    {
        // Make the platform move
        rb2d.velocity = new Vector2(-MaxSpeed, rb2d.velocity.y);
    }

    // Gets the platform ready to fall
    public void MakeUnstable()
    {
        // Calls the Fall function after FallTimer seconds
        if (!Falling)
        {
            Falling = true;
            Invoke("Fall", FallTimer);
        }
    }

    // Make the platform fall
    private void Fall()
    {
        // Make the platform Dynamic so it's affected by physics
        rb2d.bodyType = RigidbodyType2D.Dynamic;
    }

    public void ResetPlatform()
    {
        // Reset the platform's velocity, position, and rotation
        rb2d.velocity = Vector2.zero;
        transform.position = new Vector3(-100f, -100f); // Spawn it off-screen so it's out of the way
        transform.rotation = Quaternion.identity;

        // Enable the platform to become unstable again
        Falling = false;

        // Make the platform Kinematic
        rb2d.bodyType = RigidbodyType2D.Kinematic;

        // Spawn Coins
        coinSpawner.SpawnNewCoins();

        // Enable the gameObject
        gameObject.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If we collide with the water
        if (collision.gameObject.CompareTag("Water"))
        {
            // Disable the object
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // If the platform goes off the left edge of the screen
        if (collision.gameObject.CompareTag("Finish"))
        {
            // Disable the platform
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        // Fire the OnDisabled event
        OnDisabled.Invoke(gameObject);
    }
}
