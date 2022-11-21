using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllPitsFilledPuzzle : Puzzle
{
    private Pit[] pits;

    private void Start()
    {
        pits = GetComponentsInChildren<Pit>();
    }

    protected override bool IsComplete()
    {
        foreach (Pit pit in pits)
        {
            if (!pit.filled)
            {
                return false;
            }
        }
        return true;
    }
}
