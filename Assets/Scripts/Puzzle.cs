using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    private Resettable[] resettables;

    private void Start()
    {
        resettables = GetComponentsInChildren<Resettable>();
    }

    public void Reset()
    {
        foreach (Resettable resettable in resettables)
        {
            resettable.Reset();
        }
    }
}
