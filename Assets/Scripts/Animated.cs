using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animated : MonoBehaviour
{
    public float animTime = 0.3f;
    public Sprite[] sprites;
    private SpriteRenderer sr;
    private float animTimer;
    private int animFrame;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        animTimer += Time.deltaTime;
        if (animTimer >= animTime)
        {
            animTimer = 0;
            animFrame++;
            if (animFrame >= sprites.Length)
            {
                animFrame = 0;
            }
            sr.sprite = sprites[animFrame];
        }
    }
}
