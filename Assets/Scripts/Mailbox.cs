using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class Mailbox : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite closedSprite;
    [SerializeField] private Sprite openedSprite;
    [SerializeField] private ParticleSystem hitParticleSystem;

    [SerializeField] private AudioSource hitAudioSource;

    [SerializeField] private MMFeedbacks movement_MMF;
    public bool open = true;

    private GameController gameController;

    

    private void Start() {
        gameController = FindObjectOfType<GameController>();
    }
    public void Close() {
        spriteRenderer.sprite = closedSprite;
        open = false;

        // Juice it up
        hitAudioSource.Play();
        hitParticleSystem.Play();
        movement_MMF.PlayFeedbacks();
        
        
        // TODO: sound

        gameController.WinCheck();

    }

    public void Open() {
        spriteRenderer.sprite = openedSprite;
        open = true;
    }
}
