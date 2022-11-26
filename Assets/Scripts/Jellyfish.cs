using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jellyfish : Enemy
{
    private const float speed = 6;
    private Boat boat;
    private Rigidbody2D rb;
    private const float hitWaitTime = 2;
    private bool wait = false;
    private const float tetherDistance = 20;

    private void Start()
    {
        boat = FindObjectOfType<Boat>();
        rb = GetComponent<Rigidbody2D>();
        boat.canLand = false;
    }

    private void FixedUpdate()
    {
        if (wait || boat.smashed)
        {
            return;
        }
        Vector2 currentPos = rb.position;
        Vector2 boatPos = boat.transform.position;
        Vector2 diff = boatPos - currentPos;
        Vector2 dir = diff.normalized;
        if (diff.magnitude > tetherDistance)
        {
            rb.MovePosition(boatPos - dir * tetherDistance);
        }
        else
        {
            Vector2 move = speed * Time.fixedDeltaTime * dir;
            rb.MovePosition(currentPos + move);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Laser laser = collision.gameObject.GetComponent<Laser>();
        if (laser != null)
        {
            Destroy(gameObject);
            Player player = FindObjectOfType<Player>();
            player.killedJellyfish = true;
            boat.canLand = true;
            MusicManager musicManager = FindObjectOfType<MusicManager>();
            musicManager.SetMusic(null);
            //TODO spawn a ton of gold & platinum
        }
    }

    public void HitDelay()
    {
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        wait = true;
        yield return new WaitForSeconds(hitWaitTime);
        wait = false;
    }
}
