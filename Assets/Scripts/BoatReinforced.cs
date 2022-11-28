using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatReinforced : ItemGet
{
    public Sprite sprite;

    protected override void Start()
    {
        Boat boat = FindObjectOfType<Boat>();
        boat.gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        Destroy(GameObject.Find("DeepwaterBarriers"));
        base.Start();
        Destroy(gameObject);
    }
}
