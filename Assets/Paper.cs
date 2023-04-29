using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper : MonoBehaviour
{
    public float speed;
    public bool thrown = false;

    private Rigidbody2D rb;
    private Vector2 throwDirection;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
    }

    public void Throw(Vector2 direction) {
        thrown = true;
        throwDirection = direction.normalized;
        rb.velocity = throwDirection * speed;
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
