using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyPedestal : NPC
{
    public GameObject statue;
    private bool placed;

    public override void Interact(Player player)
    {
        if (player.hasJellyStatue && !placed)
        {
            OpenDialog(dialog, player);
        }
    }

    public override void DoAction(string action)
    {
        if (action == "PlaceStatue")
        {
            placed = true;
            statue.SetActive(true);
            //TODO
        }
    }
}
