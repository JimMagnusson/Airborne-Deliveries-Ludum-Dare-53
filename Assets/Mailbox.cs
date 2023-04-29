using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mailbox : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite closedSprite;
    public Sprite openedSprite;

    public bool open = true;

    public void Close() {
        spriteRenderer.sprite = closedSprite;
        open = false;
        // TODO: Juice it up
    }

    public void Open() {
        spriteRenderer.sprite = openedSprite;
        open = true;
    }
}
