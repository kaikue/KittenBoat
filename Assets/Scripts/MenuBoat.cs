using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBoat : MonoBehaviour
{
    private const float yDist = 20;
    private const float rotDist = 20;
    private RectTransform rect;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        float y = yDist * Mathf.Sin(Time.time);
        rect.anchoredPosition = new Vector2(0, y);
        float rot = rotDist * Mathf.Cos(Time.time);
        rect.rotation = Quaternion.AngleAxis(rot, Vector3.forward);
    }
}
