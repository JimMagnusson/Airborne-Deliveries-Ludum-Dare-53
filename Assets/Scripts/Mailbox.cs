using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mailbox : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite closedSprite;
    public Sprite openedSprite;

    public bool open = true;

    private GameController gameController;

    private void Start() {
        gameController = FindObjectOfType<GameController>();
    }
    public void Close() {
        spriteRenderer.sprite = closedSprite;
        open = false;
        gameController.WinCheck();
        // TODO: Juice it up
    }

    public void Open() {
        spriteRenderer.sprite = openedSprite;
        open = true;
    }
}
