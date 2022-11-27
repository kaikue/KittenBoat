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
        image.color = inactiveColor;
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("MazeStartZone"))
        {
            image.color = activeColor;
            connectedStart = true;
            inMaze = true;
        }
        if (other.CompareTag("MazeEndZone"))
        {
            image.color = activeColor;
            connectedEnd = true;
            inMaze = true;
        }
        if (other.CompareTag("MazeZone"))
        {
            image.color = activeColor;
            inMaze = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;
        MazeMarker mazeMarker = other.GetComponent<MazeMarker>();
        if (mazeMarker != null)
        {
            if (!inMaze)
            {
                rb.sleepMode = RigidbodySleepMode2D.StartAwake;
                rb.Sleep();
                return;
            }
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
