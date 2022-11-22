using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shopkeeper : NPC
{
    //main Dialog opens DialogChoice
    //DialogChoice calls DoAction("SellItem")
    public Dialog dialogSold; //calls DoAction("CollectItem")
    public Dialog dialogNoSale;
    public Dialog dialogSoldOut;
    public GameObject soldItem;
    public int price;
    public GameObject crab;

    private const float crabChance = 0.3f;

    public override void Interact(Player player)
    {
        if (flags.Contains("sold"))
        {
            OpenDialog(dialogSoldOut, player);
        }
        else
        {
            OpenDialog(dialog, player);
        }
    }

    public override void DoAction(string action)
    {
        if (action == "SellItem")
        {
            SellItem();
        }
        if (action == "CollectItem")
        {
            CollectItem();
        }
    }

    private void SellItem()
    {
        Player player = FindObjectOfType<Player>();
        if (player.coins >= price)
        {
            SetFlag("sold");
            player.AddCoins(-price);
            OpenDialog(dialogSold, player);
        }
        else
        {
            OpenDialog(dialogNoSale, player);
        }
    }

    private void CollectItem()
    {
        Player player = FindObjectOfType<Player>();
        if (Random.Range(0f, 1f) < crabChance)
        {
            Vector3 diff = transform.position - player.transform.position;
            Vector3 crabPos = transform.position + diff;
            crabPos = new Vector3(Mathf.Round(crabPos.x - 0.5f) + 0.5f, Mathf.Round(crabPos.y - 0.5f) + 0.5f, 0);
            Crab spawnedCrab = Instantiate(crab, crabPos, Quaternion.identity).GetComponent<Crab>();
            spawnedCrab.SetItem(soldItem);
        }
        else
        {
            Instantiate(soldItem);
        }
    }
}
