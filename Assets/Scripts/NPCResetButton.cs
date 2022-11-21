using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCResetButton : ResetButton
{
    public TalkingRock rock;

    public override void ResetPuzzle()
    {
        rock.ResetObj();
    }
}
