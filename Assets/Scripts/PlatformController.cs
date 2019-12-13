using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public float MaxSpeed = 1.0f;
    public float FallTimer = 1.0f;

    private Rigidbody2D rb2d;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rb2d.velocity = new Vector2(-MaxSpeed, rb2d.velocity.y);
    }

    public void MakeUnstable()
    {
        Invoke("Fall", FallTimer);
    }

    private void Fall()
    {
        rb2d.constraints = RigidbodyConstraints2D.None;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            Destroy(gameObject);
        }
    }
}
