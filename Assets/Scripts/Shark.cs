using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : Enemy
{
    public Transform[] patrolPoints;
    public int startI;
    private int goalI;
    private const float snapDist = 0.2f;
    private const float speed = 3;
    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private SpriteRenderer sr;
    private Color partialTransparent = new Color(1, 1, 1, 0.7f);
    private const float disableTime = 3f;

    private void Start()
    {
        goalI = startI;
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        Vector2 currentPos = rb.position;
        Vector2 goalPos = patrolPoints[goalI].position;
        if (Vector2.Distance(currentPos, goalPos) < snapDist)
        {
            transform.position = goalPos;
            goalI++;
            if (goalI >= patrolPoints.Length)
            {
                goalI = 0;
            }
        }
        else
        {
            Vector2 diff = goalPos - currentPos;
            Vector2 dir = diff.normalized;
            Vector2 move = speed * Time.fixedDeltaTime * dir;
            rb.MovePosition(currentPos + move);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
        }
    }

    public void TempDisable()
    {
        bc.enabled = false;
        sr.color = partialTransparent;
        StartCoroutine(ReEnable());
    }

    private IEnumerator ReEnable()
    {
        yield return new WaitForSeconds(disableTime);
        bc.enabled = true;
        sr.color = Color.white;
    }
}
