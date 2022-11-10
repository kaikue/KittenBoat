using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPad : MonoBehaviour
{
    public Sprite activeSprite;
    private Sprite inactiveSprite;
    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        inactiveSprite = sr.sprite;
    }

    public void Enter()
    {
        sr.sprite = activeSprite;
    }

    public void Exit()
    {
        sr.sprite = inactiveSprite;
    }
}
