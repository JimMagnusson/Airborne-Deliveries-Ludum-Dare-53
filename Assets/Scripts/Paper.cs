using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper : MonoBehaviour
{
    public bool thrown = false;

    [SerializeField] private GameObject hitParticles;

    private float fallMultiplier = 2.5f;

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

    public void Throw(Vector2 direction, float speed, float fallMultiplier) {
        thrown = true;
        this.speed = speed;
        throwDirection = direction.normalized;
        rb.velocity = throwDirection * this.speed;
        this.fallMultiplier = fallMultiplier;

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Mailbox")) 
        {
            if(other.gameObject.GetComponent<Mailbox>().open) {
                Mailbox mailbox = other.gameObject.GetComponent<Mailbox>();
                if(mailbox.open) {
                    mailbox.Close();
                    Destroy(this.gameObject);
                }
                else {
                    Instantiate(hitParticles, transform.position, Quaternion.identity);
                    Destroy(this.gameObject);
                }
            }
        }
        else if(other.gameObject.CompareTag("Orb")) {
            if(!other.gameObject.GetComponent<Orb>().IsRecharging()) {
                // Redirect paper toward sender
            }
        }
        else {
            Instantiate(hitParticles, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
