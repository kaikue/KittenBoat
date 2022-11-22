using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveCeiling : MonoBehaviour
{
    private const float fadeTime = 0.3f;

    private SpriteRenderer sr;
    private Coroutine crtFade;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void TryStopCoroutine(Coroutine crt)
    {
        if (crt != null)
        {
            StopCoroutine(crt);
        }
    }

    public void Enter()
    {
        TryStopCoroutine(crtFade);
        crtFade = StartCoroutine(Fade(true));
    }

    public void Exit()
    {
        TryStopCoroutine(crtFade);
        crtFade = StartCoroutine(Fade(false));
    }

    private IEnumerator Fade(bool fadeOut)
    {
        for (float t = 0; t < fadeTime; t += Time.deltaTime)
        {
            float p = t / fadeTime;
            float x = fadeOut ? 1 - p : p;
            sr.color = new Color(1, 1, 1, x);
            yield return null;
        }
        sr.color = new Color(1, 1, 1, fadeOut ? 0 : 1);
    }
}
