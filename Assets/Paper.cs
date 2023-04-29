using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper : MonoBehaviour
{
    public bool thrown = false;

    [SerializeField] private float fallMultiplier = 2.5f;

    private Rigidbody2D rb;
    private Vector2 throwDirection;

    private float speed;
    

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Remove low physics feel
        if(rb.velocity.y < 0) {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
    }

    public void Throw(Vector2 direction, float speed) {
        thrown = true;
        this.speed = speed;
        throwDirection = direction.normalized;
        rb.velocity = throwDirection * this.speed;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Mailbox")) {
            Mailbox mailbox = other.gameObject.GetComponent<Mailbox>();
            if(mailbox.open) {
                mailbox.Close();
            }
            else {
                mailbox.Open();
            }
        }
        Destroy(this.gameObject);
    }
}
