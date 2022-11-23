using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatReinforced : ItemGet
{
    protected override void Start()
    {
        Boat boat = FindObjectOfType<Boat>();
        transform.parent = boat.transform;
        transform.localPosition = Vector3.zero;
        //TODO destroy deepwater barriers
        base.Start();
    }
}
