using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mailbox : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite closedSprite;
    public Sprite openedSprite;

    public void Close() {
        spriteRenderer.sprite = closedSprite;
        // TODO: Juice it up
    }

    public void Open() {
        spriteRenderer.sprite = openedSprite;
    }
}
