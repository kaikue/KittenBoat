using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public GemPedestal.Color color;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;
        GemPedestal gemPedestal = other.GetComponent<GemPedestal>();
        if (gemPedestal != null && (gemPedestal.color == GemPedestal.Color.Any || gemPedestal.color == color))
        {
            gemPedestal.filled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;
        GemPedestal gemPedestal = other.GetComponent<GemPedestal>();
        if (gemPedestal != null && (gemPedestal.color == GemPedestal.Color.Any || gemPedestal.color == color))
        {
            gemPedestal.filled = false;
        }
    }
}
