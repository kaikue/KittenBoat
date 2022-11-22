using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    private Resettable[] resettables;
    private bool completed = false;
    public CaveDoor caveDoor;

    protected virtual void Start()
    {
        resettables = GetComponentsInChildren<Resettable>();
    }

    public void ResetPuzzle()
    {
        foreach (Resettable resettable in resettables)
        {
            resettable.ResetObj();
        }
    }

    protected virtual bool IsComplete()
    {
        return false;
    }

    private void FixedUpdate()
    {
        if (!completed && IsComplete())
        {
            completed = true;
            SolvePuzzle();
        }
    }

    private void SolvePuzzle()
    {
        caveDoor.Open();
    }
}
