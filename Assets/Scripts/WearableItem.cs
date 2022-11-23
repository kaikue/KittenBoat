using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WearableItem : ItemGet
{
    public Sprite altSprite;
    private Sprite sprite;
    private Player player;
    private SpriteRenderer sr;
    private SpriteRenderer playerSR;
    
    protected override void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sprite = sr.sprite;
        player = FindObjectOfType<Player>();
        playerSR = player.gameObject.GetComponent<SpriteRenderer>();
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;
        base.Start();
    }

    private void LateUpdate()
    {
        sr.flipX = playerSR.flipX;
        if (altSprite)
        {
            sr.sprite = player.walkSpriteUsed ? altSprite : sprite;
        }
    }
}
