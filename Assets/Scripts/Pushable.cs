using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushable : Resettable
{
    private Vector2 size;
    private Rigidbody2D rb;

    protected override void Start()
    {
        base.Start();
        size = new Vector2(0.5f, 0.5f);
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector2.zero;
    }

    public bool PushDirection(Vector2 direction)
    {
        Vector2 newPos = rb.position + direction.normalized;
        Collider2D collider = Physics2D.OverlapBox(newPos, size, 0, LayerMask.GetMask("BoatWalls", "Player", "RockTiles", "WaterTiles", "Pushable"));
        if (collider == null)
        {
            rb.MovePosition(newPos);
            return true;
        }
        return false;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;
        Pit pit = other.GetComponent<Pit>();
        if (pit != null)
        {
            boxCollider.enabled = false;
            sr.enabled = false;
            pit.Fill();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;
        HiddenSpot hiddenSpot = other.GetComponent<HiddenSpot>();
        if (hiddenSpot != null)
        {
            hiddenSpot.filled = true;
        }
        ResetButton resetButton = other.GetComponent<ResetButton>();
        if (resetButton != null)
        {
            resetButton.ResetPuzzle();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;
        HiddenSpot hiddenSpot = other.GetComponent<HiddenSpot>();
        if (hiddenSpot != null)
        {
            hiddenSpot.filled = false;
        }
    }
}
