
using UnityEngine;
using MoreMountains.Feedbacks;

// A simple 2D movement controller for a player in Unity
public class PlayerMovementController : MonoBehaviour
{
    #region Gameplay properties

    // Horizontal player keyboard input
    //  -1 = Left
    //   0 = No input
    //   1 = Right
    private float playerInput = 0;

    // Horizontal player speed
    [SerializeField] private float speed = 250;

    [SerializeField] private float jumpVelocity = 2;

    [SerializeField] private float launchVelocity = 4;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2.0f;

    [SerializeField] private MMFeedbacks movement_MMF;

    public bool airborne = false;
    public bool launched = false;
    #endregion

    #region Component references

    private Rigidbody2D rb;

    #endregion

    #region Initialisation methods

    // Initialises this component
    // (NB: Is called automatically before the first frame update)
    void Start()
    {
        // Get component references
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Mailbox")) {
            //launched = false;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Mailbox")) {
            //airborne = true;
            //launched = false;
        }
    }

    #endregion

    #region Gameplay methods

    // Is called automatically every graphics frame
    void Update()
    {
        // Detect and store horizontal player input   
        playerInput = Input.GetAxisRaw("Horizontal");
        if(playerInput != 0 && !airborne) {
            movement_MMF.PlayFeedbacks();
        }
        else {
            movement_MMF.StopFeedbacks();
        }
        if(airborne) {
            //movement_MMF.ResetFeedbacks(); - not sure how to reset them
            movement_MMF.StopFeedbacks();
        }

        // NB: Here, you might want to set the player's animation,
        // e.g. idle or walking

        // Swap the player sprite scale to face the movement direction
        SwapSprite();

        // Move the player horizontally
        // If player tries to move in same direction as before, take the max of current velocity x (could be higher due to launches) and playerInput
        if((playerInput == 0 || playerInput * rb.velocity.x > 0) && launched) { // if same sign & launched
            if(rb.velocity.x > 0) {
                rb.velocity = new Vector2(Mathf.Max(playerInput * speed, rb.velocity.x), rb.velocity.y);
            }
            else {
                rb.velocity = new Vector2(Mathf.Min(playerInput * speed, rb.velocity.x), rb.velocity.y);
            }
            
        }
        // Otherwise, let the player immediately change direction. Feels better
        else {
            rb.velocity = new Vector2(playerInput * speed, rb.velocity.y);
            launched = false;
        }
        

        if(Input.GetButton("Jump") && !airborne) {
            rb.velocity = new Vector2(rb.velocity.x, Vector2.up.y * jumpVelocity);
            airborne = true;
        }

        if(rb.velocity.y < 0) {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump")) {
            rb.velocity +=  Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    // Swap the player sprite scale to face the movement direction
    void SwapSprite()
    {
        // Right
        if (playerInput > 0)
        {
            transform.localScale = new Vector3(
                Mathf.Abs(transform.localScale.x),
                transform.localScale.y,
                transform.localScale.z
            );
        }
        // Left
        else if (playerInput < 0)
        {
            transform.localScale = new Vector3(
                -1 * Mathf.Abs(transform.localScale.x),
                transform.localScale.y,
                transform.localScale.z
            );
        }
    }

    public void LaunchToward(Vector2 direction) {
        if(airborne) {
            rb.velocity = direction * launchVelocity;
            launched = true;
        }
    }

    #endregion
}
