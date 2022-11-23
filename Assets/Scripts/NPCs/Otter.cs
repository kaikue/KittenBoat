using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Otter : NPC
{
    //main Dialog calls DoAction("GiveLetter")
    public Dialog dialogGivenLetter;
    public Dialog dialogLetterDelivered; //calls DoAction("FinishPuzzle") and sets flag "letterDeliveredAcknowledged"
    public Dialog dialogLetterDeliveredAcknowledged;
    public Sprite blushSprite;
    public Puzzle puzzle;

    private SpriteRenderer sr;

    protected override void Start()
    {
        base.Start();
        sr = GetComponent<SpriteRenderer>();
    }

    public override void Interact(Player player)
    {
        if (flags.Contains("letterDeliveredAcknowledged"))
        {
            OpenDialog(dialogLetterDeliveredAcknowledged, player);
        }
        else if (player.deliveredLetter)
        {
            OpenDialog(dialogLetterDelivered, player);
            sr.sprite = blushSprite;
        }
        else if (player.hasLetter)
        {
            OpenDialog(dialogGivenLetter, player);
        }
        else
        {
            OpenDialog(dialog, player);
        }
    }

    public override void DoAction(string action)
    {
        Player player = FindObjectOfType<Player>();
        if (action == "GiveLetter")
        {
            player.hasLetter = true;
        }
        else if (action == "FinishPuzzle")
        {
            puzzle.SolvePuzzle();
        }
    }
}
