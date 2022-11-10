using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pit : Resettable
{
    public Sprite fillSprite;
    [HideInInspector]
    public bool filled = false;

    public void Fill()
    {
        sr.sprite = fillSprite;
        boxCollider.enabled = false;
        filled = true;
    }

    public override void Reset()
    {
        base.Reset();
        filled = false;
    }
}
