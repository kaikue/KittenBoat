using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemPedestal : MonoBehaviour
{
    [HideInInspector]
    public bool filled = false;

    public Color color;

    public enum Color
    {
        Any,
        Red,
        Blue,
        Green,
        Yellow
    }
}
