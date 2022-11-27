using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MazeMarker : MonoBehaviour
{
    public Color activeColor;
    public Color inactiveColor;
    [HideInInspector]
    public Maze maze;

    private Image image;
    private bool connectedStart;
    private bool connectedEnd;
    private Rigidbody2D rb;
    private bool inMaze;

    private void Start()
    {
        image = GetComponent<Image>();
        rb = GetComponent<Rigidbody2D>();
        image.color = inactiveColor;
        Collider2D[] overlap = Physics2D.OverlapBoxAll(transform.position, GetComponent<BoxCollider2D>().size, 0, LayerMask.GetMask("UI"));
        foreach (Collider2D coll in overlap)
        {
            GameObject other = coll.gameObject;
            if (other.CompareTag("MazeStartZone") ||
                other.CompareTag("MazeEndZone") ||
                other.CompareTag("MazeZone"))
            {
                image.color = activeColor;
                inMaze = true;
            }
            if (other.CompareTag("MazeStartZone"))
            {
                connectedStart = true;
            }
            if (other.CompareTag("MazeEndZone"))
            {
                connectedEnd = true;
            }
        }
        if (!inMaze)
        {
            rb.sleepMode = RigidbodySleepMode2D.StartAwake;
            rb.Sleep();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;
        MazeMarker mazeMarker = other.GetComponent<MazeMarker>();
        if (mazeMarker != null && inMaze)
        {
            if (mazeMarker.connectedStart)
            {
                connectedStart = true;
                rb.sleepMode = RigidbodySleepMode2D.StartAwake;
                rb.Sleep();
            }
            if (mazeMarker.connectedEnd)
            {
                connectedEnd = true;
                rb.sleepMode = RigidbodySleepMode2D.StartAwake;
                rb.Sleep();
            }
            if (connectedStart && connectedEnd)
            {
                maze.Solve();
            }
        }
    }
}
