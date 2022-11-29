using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleResetZone : MonoBehaviour
{
    public ResetButton resetButton;

    private void Start()
    {
        if (resetButton == null)
        {
            print("Missing reset button! " + this);
        }
    }
}
