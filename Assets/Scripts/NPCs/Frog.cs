using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : NPC
{
    public Dialog dialogRetalk;

    public override void Interact(Player player)
    {
        if (flags.Contains("talked"))
        {
            OpenDialog(dialogRetalk, player);
        }
        else
        {
            OpenDialog(dialog, player);
        }
    }
}
