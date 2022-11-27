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
    private Player player;
    public GameObject silverCoin;
    public GameObject goldCoin;
    public GameObject platinumCoin;
    private const float coinForce = 40;
    private const float coinDist = 5;
    private const int numEachCoin = 200;
    public GameObject deathSounds;

    private void Start()
    {
        boat = FindObjectOfType<Boat>();
        player = FindObjectOfType<Player>();
        rb = GetComponent<Rigidbody2D>();
        boat.canLand = false;
    }

    private void FixedUpdate()
    {
        if (wait || boat.smashed || player.paused)
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
            player.GetComponent<AudioSource>().volume = 0.2f; //hacky but WHATEVER...
            boat.canLand = true;
            MusicManager musicManager = FindObjectOfType<MusicManager>();
            musicManager.SetMusic(null);
            SpawnLoot();
            Instantiate(deathSounds);
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

    private void SpawnLoot()
    {
        GameObject[] coins = { silverCoin, goldCoin, platinumCoin };
        foreach (GameObject coin in coins)
        {
            for (int i = 0; i < numEachCoin; i++)
            {
                float angle = Mathf.Deg2Rad * Random.Range(0, 360);
                float r = Random.Range(0, coinDist);
                Vector3 position = rb.position + new Vector2(r * Mathf.Cos(angle), r * Mathf.Sin(angle));
                GameObject spawnedCoin = Instantiate(coin, position, Quaternion.identity);
                Rigidbody2D coinRB = spawnedCoin.GetComponent<Rigidbody2D>();
                coinRB.constraints = RigidbodyConstraints2D.FreezeRotation;
                coinRB.AddForce(new Vector2(Random.Range(-coinForce, coinForce), Random.Range(-coinForce, coinForce)));
            }
        }
    }
}
