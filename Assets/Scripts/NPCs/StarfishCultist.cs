using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarfishCultist : Shopkeeper
{
    public Dialog dialogKilledJellyfish;

    public override void Interact(Player player)
    {
        if (player.killedJellyfish)
        {
            OpenDialog(dialogKilledJellyfish, player);
        }
        else
        {
            base.Interact(player);
        }
    }
}
