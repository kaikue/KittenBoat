using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePad : MonoBehaviour
{
    public const float force = 4;
    public Vector2 direction;
    
    [HideInInspector]
    public Boat boat;

    private void Start()
    {
        boat = GetComponentInParent<Boat>();
    }
}
