using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaSerpent : Shopkeeper
{
    public Dialog dialogNeedClothes;

    protected override void Start()
    {
        crabChance = 0;
        base.Start();
    }

    public override void Interact(Player player)
    {
        bool piratey = player.GetComponentsInChildren<WearableItem>().Length >= 4;
        if (!piratey)
        {
            OpenDialog(dialogNeedClothes, player);
        }
        else
        {
            base.Interact(player);
        }
    }
}
