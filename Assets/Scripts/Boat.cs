using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boat : MonoBehaviour
{
    private const float maxSpeed = 7;

    private Sprite flagInactiveSprite;
    public Sprite flagOpenSprite;
    public Collider2D boatWallTop;
    public Collider2D boatWallBottom;
    public Collider2D boatWallLeft;
    public Collider2D boatWallRight;
    public Rigidbody2D rb;
    public const int maxHealth = 3;
    public int health = maxHealth;
    public RectTransform heartsUI;
    public GameObject[] emptyHearts;
    private Coroutine crtShowHearts;
    private const int hideHeartsY = 270;
    private const int showHeartsY = 185;
    private const float moveHeartsTime = 0.5f;
    private AudioSource hitSound;

    public SpriteRenderer flagRenderer;

    private void Start()
    {
        flagInactiveSprite = flagRenderer.sprite;
        rb = GetComponent<Rigidbody2D>();
        hitSound = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        bool leftHit = Physics2D.Raycast(rb.position + new Vector2(-2.575f, -0.875f / 2), Vector2.up, 0.875f, LayerMask.GetMask("LandTiles")).collider != null;
        boatWallLeft.enabled = !leftHit;
        bool rightHit = Physics2D.Raycast(rb.position + new Vector2(2.575f, -0.875f / 2), Vector2.up, 0.875f, LayerMask.GetMask("LandTiles")).collider != null;
        boatWallRight.enabled = !rightHit;
        bool topHit = Physics2D.Raycast(rb.position + new Vector2(-1.625f / 2, 2.2f), Vector2.right, 1.625f, LayerMask.GetMask("LandTiles")).collider != null;
        boatWallTop.enabled = !topHit;
        bool bottomHit = Physics2D.Raycast(rb.position + new Vector2(-1.625f / 2, -2.2f), Vector2.right, 1.625f, LayerMask.GetMask("LandTiles")).collider != null;
        boatWallBottom.enabled = !bottomHit;
        if (leftHit || rightHit || topHit || bottomHit) {
            flagRenderer.sprite = flagInactiveSprite;
        }
        else
        {
            flagRenderer.sprite = flagOpenSprite;
        }

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        LaserButton laserButton = collision.gameObject.GetComponent<LaserButton>();
        if (laserButton != null)
        {
            laserButton.Enter();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        LaserButton laserButton = collision.gameObject.GetComponent<LaserButton>();
        if (laserButton != null)
        {
            laserButton.Exit();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            Piranha piranha = other.GetComponent<Piranha>();
            if (piranha != null)
            {
                Destroy(other);
            }
            Shark shark = other.GetComponent<Shark>();
            if (shark != null)
            {
                shark.TempDisable();
            }
            Jellyfish jellyfish = other.GetComponent<Jellyfish>();
            if (jellyfish != null)
            {
                //TODO move jelly back a bit
            }
            Damage();
        }
    }

    private void Damage()
    {
        hitSound.Play();
        health--;
        for (int i = 0; i < maxHealth; i++)
        {
            emptyHearts[i].SetActive(i >= health);
        }
        ShowHearts();
        if (health <= 0)
        {
            //TODO smash boat
            Restore();
        }
    }

    private void Restore()
    {
        health = maxHealth;
        HideHearts();
        for (int i = 0; i < maxHealth; i++)
        {
            emptyHearts[i].SetActive(false);
        }
    }

    private void TryStopCoroutine(Coroutine crt)
    {
        if (crt != null)
        {
            StopCoroutine(crt);
        }
    }

    private void ShowHearts()
    {
        TryStopCoroutine(crtShowHearts);
        crtShowHearts = StartCoroutine(SlideHeartsIn());
    }

    private void HideHearts()
    {
        TryStopCoroutine(crtShowHearts);
        crtShowHearts = StartCoroutine(SlideHeartsOut());
    }

    private IEnumerator SlideHeartsIn()
    {
        float startY = Mathf.Min(hideHeartsY, heartsUI.anchoredPosition.y); 

        for (float t = 0; t < moveHeartsTime; t += Time.deltaTime)
        {
            float y = Mathf.Lerp(startY, showHeartsY, t / moveHeartsTime);
            heartsUI.anchoredPosition = new Vector2(heartsUI.anchoredPosition.x, y);
            yield return null;
        }
        heartsUI.anchoredPosition = new Vector2(heartsUI.anchoredPosition.x, showHeartsY);
    }

    private IEnumerator SlideHeartsOut()
    {
        float delayTime = 1;

        yield return new WaitForSeconds(delayTime);
        for (float t = 0; t < moveHeartsTime; t += Time.deltaTime)
        {
            float y = Mathf.Lerp(showHeartsY, hideHeartsY, t / moveHeartsTime);
            heartsUI.anchoredPosition = new Vector2(heartsUI.anchoredPosition.x, y);
            yield return null;
        }
        heartsUI.anchoredPosition = new Vector2(heartsUI.anchoredPosition.x, hideHeartsY);
    }
}
