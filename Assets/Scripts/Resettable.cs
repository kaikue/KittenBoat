using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resettable : MonoBehaviour
{
    private Vector3 startPos;
    protected BoxCollider2D boxCollider;
    protected SpriteRenderer sr;
    private Sprite originalSprite;

    protected virtual void Start()
    {
        startPos = transform.position;
        boxCollider = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            originalSprite = sr.sprite;
        }
    }

    public virtual void ResetObj()
    {
        transform.position = startPos;
        if (boxCollider != null)
        {
            boxCollider.enabled = true;
        }
        if (sr != null)
        {
            sr.enabled = true;
            sr.sprite = originalSprite;
        }
    }
}
