using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuriedSecret : NPC
{
    public Puzzle puzzle;
    private bool solvedPuzzle;

    public override void Interact(Player player)
    {
        if (!solvedPuzzle)
        {
            OpenDialog(dialog, player);
        }
    }

    public override void DoAction(string action)
    {
        if (action == "SolvePuzzle")
        {
            puzzle.SolvePuzzle();
            solvedPuzzle = true;
        }
    }
}
