using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piranha : Enemy
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Laser laser = collision.gameObject.GetComponent<Laser>();
        if (laser != null)
        {
            laser.killedPiranhas++;
            Destroy(gameObject);
        }
    }
}
