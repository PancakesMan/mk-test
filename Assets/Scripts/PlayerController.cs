using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    // Custom event classes
    [System.Serializable]
    public class DeathEvent : UnityEvent { }
    [System.Serializable]
    public class PickupEvent : UnityEvent<Collectable> { }

    public float JumpForce = 1.0f;           // Force applied when we jump
    public DeathEvent OnDeath;               // Event fired when we die
    public PickupEvent OnPickupCollectable;  // Event fired when we collect a collectable

    private Animator animator; // Animator for the Player
    private Rigidbody2D rb2d;  // RIgidbody2D for the Player

    private bool Jumping = false; // Are we jumping?
    private bool Falling = false; // Are we falling?

    // Start is called before the first frame update
    void Start()
    {
        // Get the Animator component
        animator = GetComponent<Animator>();

        // Get the Rigidbody2D component
        rb2d = GetComponent<Rigidbody2D>();

        // Start the running animation
        animator.SetBool("Playing", true);
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
        // If we touch a collectable
        if (collision.CompareTag("Collectable"))
        {
            // Get the Collectable script component on the object
            Collectable collectable = collision.GetComponent<Collectable>();

            // Fire the event and send the script object through
            OnPickupCollectable.Invoke(collectable);
        }

        // If we touch the water
        if (collision.CompareTag("Water"))
        {
            // We died, stop animating the player
            animator.SetBool("Playing", false);

            // Fire the OnDeath event
            OnDeath.Invoke();
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
