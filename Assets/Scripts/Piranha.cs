using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piranha : Enemy
{
    private const float speed = 3;
    private const float despawnDistance = 20;
    private Boat boat;
    private Player player;
    private Rigidbody2D rb;

    private void Start()
    {
        boat = FindObjectOfType<Boat>();
        player = FindObjectOfType<Player>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (player.paused) return;

        Vector2 currentPos = rb.position;
        Vector2 diff = boat.transform.position - transform.position;
        if (diff.magnitude > despawnDistance || boat.smashed || player.gameObject.layer != LayerMask.NameToLayer("PlayerBoat"))
        {
            Destroy(gameObject);
            return;
        }
        Vector2 dir = diff.normalized;
        Vector2 move = speed * Time.fixedDeltaTime * dir;
        rb.MovePosition(currentPos + move);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Laser laser = collision.gameObject.GetComponent<Laser>();
        if (laser != null)
        {
            laser.killedPiranhas++;
            Destroy(gameObject);
        }
    }
}
