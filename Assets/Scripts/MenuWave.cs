using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuWave : MonoBehaviour
{
    private const float speed = 30;
    private RectTransform rect;
    private float offscreenDist;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        offscreenDist = 400 + rect.sizeDelta.x / 2;
    }

    private void Update()
    {
        rect.anchoredPosition += new Vector2(-speed * Time.deltaTime, 0);
        if (rect.anchoredPosition.x < -offscreenDist)
        {
            rect.anchoredPosition += new Vector2(offscreenDist * 2, 0);
        }
    }
}
