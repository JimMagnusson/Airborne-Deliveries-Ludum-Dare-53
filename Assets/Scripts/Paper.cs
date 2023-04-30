using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper : MonoBehaviour
{
    public bool thrown = false;

    [SerializeField] private GameObject hitParticles;

    [SerializeField] private float returningSpeed = 2.0f;

    private float fallMultiplier = 2.5f;

    private bool returned = false;

    private Rigidbody2D rb;

    private TrailRenderer trailRenderer;
    private Vector2 throwDirection;

    private float speed;

    private bool returning = false;

    public Player player; // is set when spawned in Player.cs
    

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        trailRenderer = GetComponent<TrailRenderer>();
    }

    public bool IsReturning() {
        return returning;
    }

    void Update()
    {
        // Remove low physics feel
        if(!returning) {
            if(rb.velocity.y < 0) {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
        }
        else {
            // Go toward player

            Vector2 paperToPlayer = player.transform.position - transform.position;
            // To fix bug if enter2d doesnt fire
            if(paperToPlayer.magnitude <= 0.5f) {
                HandleReturnToPlayer();
            }
            rb.velocity = paperToPlayer.normalized * returningSpeed;
        }
    }

    public void StartReturningPaper() {
        returning = true;
        trailRenderer.enabled = true;
    }

    public void Throw(Vector2 direction, float speed, float fallMultiplier) {
        thrown = true;
        this.speed = speed;
        throwDirection = direction.normalized;
        rb.velocity = throwDirection * this.speed;
        this.fallMultiplier = fallMultiplier;

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Mailbox") && !returning) 
        {
            if(other.gameObject.GetComponent<Mailbox>().open) {

                Mailbox mailbox = other.gameObject.GetComponent<Mailbox>();
                if(mailbox.open) {
                    mailbox.Close();
                    player.RemoveFromReturnablePapers(this);
                    Destroy(this.gameObject);
                }
                else {
                    Instantiate(hitParticles, transform.position, Quaternion.identity);
                }
            }
        }
        else if(other.gameObject.CompareTag("Orb") && !returning) {
            if(!other.gameObject.GetComponent<Orb>().IsRecharging()) {
                StopMoving();
                StartReturningPaper();
            }
        }
        else if(other.gameObject.CompareTag("Player")) {
            if(returning) {
                HandleReturnToPlayer();
            }
        }
        else if(!returning){
            Instantiate(hitParticles, transform.position, Quaternion.identity);
            StopMoving();
            player.AddToReturnablePapers(this);
        }
    }
    private void StopMoving() {
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
    }

    private void HandleReturnToPlayer() {
        if(!returned) {
            returned = true;
            player.RegainPaper(this);
            Destroy(this.gameObject);
        }
    }
}
