using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Boat : MonoBehaviour
{
    private const float maxSpeed = 7;

    public SpriteRenderer flagRenderer;
    private Sprite flagInactiveSprite;
    public Sprite flagOpenSprite;
    public Collider2D boatWallTop;
    public Collider2D boatWallBottom;
    public Collider2D boatWallLeft;
    public Collider2D boatWallRight;
    [HideInInspector]
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
    [HideInInspector]
    public bool smashed;
    [HideInInspector]
    public bool canLand = true;
    [HideInInspector]
    public bool jellyfishSummoned;
    private const int jellyfishSummonTime = 10;
    private const float jellySpawnDist = 20;
    public Jellyfish jellyfishPrefab;
    public TextMeshProUGUI timerText;

    private void Start()
    {
        flagInactiveSprite = flagRenderer.sprite;
        rb = GetComponent<Rigidbody2D>();
        hitSound = GetComponent<AudioSource>();
        canLand = true;
    }

    private void FixedUpdate()
    {
        bool leftHit = Physics2D.Raycast(rb.position + new Vector2(-2.575f, -0.875f / 2), Vector2.up, 0.875f, LayerMask.GetMask("LandTiles")).collider != null;
        boatWallLeft.enabled = !canLand || !leftHit;
        bool rightHit = Physics2D.Raycast(rb.position + new Vector2(2.575f, -0.875f / 2), Vector2.up, 0.875f, LayerMask.GetMask("LandTiles")).collider != null;
        boatWallRight.enabled = !canLand || !rightHit;
        bool topHit = Physics2D.Raycast(rb.position + new Vector2(-1.625f / 2, 2.2f), Vector2.right, 1.625f, LayerMask.GetMask("LandTiles")).collider != null;
        boatWallTop.enabled = !canLand || !topHit;
        bool bottomHit = Physics2D.Raycast(rb.position + new Vector2(-1.625f / 2, -2.2f), Vector2.right, 1.625f, LayerMask.GetMask("LandTiles")).collider != null;
        boatWallBottom.enabled = !canLand || !bottomHit;
        if (canLand && (leftHit || rightHit || topHit || bottomHit)) {
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

        if (jellyfishSummoned && boatWallLeft.enabled && boatWallRight.enabled && boatWallLeft.enabled && boatWallTop.enabled)
        {
            jellyfishSummoned = false;
            StartCoroutine(SummonJellyfish());
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
                jellyfish.HitDelay();
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
            smashed = true;
            //TODO smash boat
            Restore();
        }
    }

    private void Restore()
    {
        smashed = false;
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

    private IEnumerator SummonJellyfish()
    {
        print("SUMMONING");
        canLand = false;
        MusicManager musicManager = FindObjectOfType<MusicManager>();
        musicManager.SetMusic(null);
        for (int i = jellyfishSummonTime; i >= 0; i--)
        {
            timerText.text = "00:" + (i < 10 ? "0" : "") + i;
            yield return new WaitForSeconds(1);
        }
        timerText.text = "";
        Instantiate(jellyfishPrefab, transform.position + Vector3.down * jellySpawnDist, Quaternion.identity);
        musicManager.SetMusic(musicManager.bossMusicSrc);
    }
}
