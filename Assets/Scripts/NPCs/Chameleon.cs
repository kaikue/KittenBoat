using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chameleon : NPC
{
    //main Dialog sets flag "talked"
    public Dialog dialogRetalk;
    public Dialog dialogComplete; //sets flag "completeTalked" and calls action "FinishPuzzle"
    public Dialog dialogCompleteRetalk;
    public Dialog dialogKilledJellyfish;
    public Puzzle puzzle;
    public Laser laser;

    public override void Interact(Player player)
    {
        if (player.killedJellyfish)
        {
            OpenDialog(dialogKilledJellyfish, player);
        }
        else if (flags.Contains("completeTalked"))
        {
            OpenDialog(dialogRetalk, player);
        }
        else if (laser.killedPiranhas >= 5)
        {
            OpenDialog(dialogComplete, player);
        }
        else if (flags.Contains("talked"))
        {
            OpenDialog(dialogRetalk, player);
        }
        else
        {
            OpenDialog(dialog, player);
        }
    }

    public override void DoAction(string action)
    {
        if (action == "FinishPuzzle")
        {
            puzzle.SolvePuzzle();
        }
    }
}
