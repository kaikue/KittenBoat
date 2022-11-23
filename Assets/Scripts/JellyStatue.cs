using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyStatue : ItemGet
{
    protected override void Start()
    {
        base.Start();
        Player player = FindObjectOfType<Player>();
        player.hasJellyStatue = true;
        Destroy(gameObject);
    }
}
