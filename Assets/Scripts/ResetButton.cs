using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetButton : MonoBehaviour
{
    [HideInInspector]
    private Puzzle puzzle;

    private void Start()
    {
        puzzle = GetComponentInParent<Puzzle>();
    }

    public virtual void ResetPuzzle()
    {
        puzzle.ResetPuzzle();
        GetComponent<AudioSource>().Play();
    }
}
