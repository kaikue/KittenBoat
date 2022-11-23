using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllHiddenSpotsFilledPuzzle : Puzzle
{
    private HiddenSpot[] hiddenSpots;

    protected override void Start()
    {
        hiddenSpots = GetComponentsInChildren<HiddenSpot>();
        base.Start();
    }

    protected override bool IsComplete()
    {
        foreach (HiddenSpot hiddenSpots in hiddenSpots)
        {
            if (!hiddenSpots.filled)
            {
                return false;
            }
        }
        return true;
    }
}
