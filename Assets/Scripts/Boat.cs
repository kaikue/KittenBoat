using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    private const float maxSpeed = 7;

    private Sprite flagInactiveSprite;
    public Sprite flagOpenSprite;
    public Collider2D boatWallTop;
    public Collider2D boatWallBottom;
    public Collider2D boatWallLeft;
    public Collider2D boatWallRight;
    public Rigidbody2D rb;

    public SpriteRenderer flagRenderer;

    private void Start()
    {
        flagInactiveSprite = flagRenderer.sprite;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        bool leftHit = Physics2D.Raycast(rb.position + new Vector2(-2.575f, -0.875f / 2), Vector2.up, 0.875f, LayerMask.GetMask("LandTiles")).collider != null;
        boatWallLeft.enabled = !leftHit;
        bool rightHit = Physics2D.Raycast(rb.position + new Vector2(2.575f, -0.875f / 2), Vector2.up, 0.875f, LayerMask.GetMask("LandTiles")).collider != null;
        boatWallRight.enabled = !rightHit;
        bool topHit = Physics2D.Raycast(rb.position + new Vector2(-1.625f / 2, 2.2f), Vector2.right, 1.625f, LayerMask.GetMask("LandTiles")).collider != null;
        boatWallTop.enabled = !topHit;
        bool bottomHit = Physics2D.Raycast(rb.position + new Vector2(-1.625f / 2, -2.2f), Vector2.right, 1.625f, LayerMask.GetMask("LandTiles")).collider != null;
        boatWallBottom.enabled = !bottomHit;
        if (leftHit || rightHit || topHit || bottomHit) {
            flagRenderer.sprite = flagInactiveSprite;
        }
        else
        {
            flagRenderer.sprite = flagOpenSprite;
        }

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        LaserButton laserButton = collision.gameObject.GetComponent<LaserButton>();
        if (laserButton != null)
        {
            laserButton.Enter();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        LaserButton laserButton = collision.gameObject.GetComponent<LaserButton>();
        if (laserButton != null)
        {
            laserButton.Exit();
        }
    }
}
