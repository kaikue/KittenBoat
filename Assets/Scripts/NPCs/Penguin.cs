using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penguin : NPC
{
    public Dialog dialogReceivesLetter; //calls DoAction("GetLetter"), sets flag "letterReceivedAcknowledged"
    public Dialog dialogLetterReceivedAcknowledged;
    public Sprite blushSprite;

    private SpriteRenderer sr;

    protected override void Start()
    {
        base.Start();
        sr = GetComponent<SpriteRenderer>();
    }

    public override void Interact(Player player)
    {
        if (flags.Contains("letterReceivedAcknowledged"))
        {
            OpenDialog(dialogLetterReceivedAcknowledged, player);
        }
        else if (player.hasLetter)
        {
            OpenDialog(dialogReceivesLetter, player);
            sr.sprite = blushSprite;
        }
        else
        {
            OpenDialog(dialog, player);
        }
    }

    public override void DoAction(string action)
    {
        Player player = FindObjectOfType<Player>();
        if (action == "GetLetter")
        {
            player.deliveredLetter = true;
        }
    }
}
