using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkingRock : NPC
{
    public Dialog dialog2;
    public Dialog dialog3;
    public Dialog dialog4;
    public Dialog dialogReset;
    public Pit pit;

    public override void Interact(Player player)
    {
        if (flags.Contains("talked3"))
        {
            if (flags.Contains("reset"))
            {
                OpenDialog(dialogReset, player);
            }
            else
            {
                OpenDialog(dialog4, player);
            }
        }
        else if (flags.Contains("talked2"))
        {
            OpenDialog(dialog3, player);
        }
        else if (flags.Contains("talked1"))
        {
            OpenDialog(dialog2, player);
        }
        else
        {
            OpenDialog(dialog, player);
        }
    }

    public override void SetFlag(string flag)
    {
        if (flag == "talked4")
        {
            FinallyMove();
        }
        base.SetFlag(flag);
    }

    public void FinallyMove()
    {
        pit.Fill();
        gameObject.SetActive(false);
    }

    public void ResetObj()
    {
        if (flags.Contains("talked4") && !flags.Contains("reset"))
        {
            gameObject.SetActive(true);
            pit.ResetObj();
            SetFlag("reset");
        }
    }
}
