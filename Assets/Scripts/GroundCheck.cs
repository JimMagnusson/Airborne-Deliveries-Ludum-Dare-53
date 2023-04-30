using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private PlayerMovementController playerMovementController;
    private Player player;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        playerMovementController = GetComponentInParent<PlayerMovementController>();
        player = GetComponentInParent<Player>();
        rb = GetComponentInParent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Mailbox")) {
            playerMovementController.airborne = false;
            playerMovementController.launched = false;
            player.ResetPapersLeft();
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Mailbox")) {
            playerMovementController.airborne = true;
            //launched = false;
        }
    }
}
