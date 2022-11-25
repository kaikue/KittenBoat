using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllGemPedestalsFilledPuzzle : Puzzle
{
    private GemPedestal[] gemPedestals;

    protected override void Start()
    {
        gemPedestals = GetComponentsInChildren<GemPedestal>();
        base.Start();
    }

    protected override bool IsComplete()
    {
        foreach (GemPedestal gemPedestal in gemPedestals)
        {
            if (!gemPedestal.filled)
            {
                return false;
            }
        }
        return true;
    }
}
