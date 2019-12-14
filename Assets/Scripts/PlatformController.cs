using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public float MaxSpeed = 1.0f;  // Speed at which the platform moves
    public float FallTimer = 1.0f; // How long it takes to fall after BecomeUnstable is called

    public Transform EndPosition;  // Point on the right-hand edge of the platform

    private Rigidbody2D rb2d;      // Rigidbody2D of the platform

    void Awake()
    {
        // Get the Rigidbody2D component
        rb2d = GetComponent<Rigidbody2D>();
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
        Invoke("Fall", FallTimer);
    }

    // Make the platform fall
    private void Fall()
    {
        // Unlock the Y constraint on the Ridigbody2D
        rb2d.constraints = RigidbodyConstraints2D.None;
    }

    private void OnBecameInvisible()
    {
        // Destroy the platform when it goes off the left of the screen
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If we collide with the water
        if (collision.gameObject.CompareTag("Water"))
        {
            // Destroy the platform
            Destroy(gameObject);
        }
    }
}
