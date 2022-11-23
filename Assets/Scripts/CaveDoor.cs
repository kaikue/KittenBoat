using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveDoor : MonoBehaviour
{
    public Sprite openSprite;
    private SpriteRenderer sr;
    private BoxCollider2D boxCollider;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void Open()
    {
        boxCollider.enabled = false;
        sr.sprite = openSprite;
    }
}
