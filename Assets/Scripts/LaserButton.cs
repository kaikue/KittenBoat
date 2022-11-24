using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserButton : MonoBehaviour
{
    public GameObject laser;
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
        laser.SetActive(true);
    }

    public void Exit()
    {
        sr.sprite = inactiveSprite;
        laser.SetActive(false);
    }
}
