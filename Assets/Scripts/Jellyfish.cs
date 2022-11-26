using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jellyfish : Enemy
{
    private const float speed = 3;
    private Boat boat;
    private Rigidbody2D rb;

    private void Start()
    {
        boat = FindObjectOfType<Boat>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 currentPos = rb.position;
        Vector2 diff = boat.transform.position - transform.position;
        Vector2 dir = diff.normalized;
        Vector2 move = speed * Time.fixedDeltaTime * dir;
        rb.MovePosition(currentPos + move);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Laser laser = collision.gameObject.GetComponent<Laser>();
        if (laser != null)
        {
            Destroy(gameObject);
            Player player = FindObjectOfType<Player>();
            player.killedJellyfish = true;
            //TODO stop music, spawn a ton of gold & platinum
        }
    }
}
