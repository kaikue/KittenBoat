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
    public GameObject mazePrefab;
    public Transform hudCanvas;
    private Player player;
    private bool invincible;
    private SpriteRenderer sr;
    private Color partialTransparent = new Color(1, 1, 1, 0.7f);
    private const float invincibleTime = 4;
    public Sprite smashedSprite;
    private Sprite unsmashedSprite;
    public GameObject boatRestorePrompt;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        flagInactiveSprite = flagRenderer.sprite;
        rb = GetComponent<Rigidbody2D>();
        hitSound = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
        canLand = true;
    }

    private void FixedUpdate()
    {
        if (smashed || player.paused)
        {
            rb.velocity = Vector2.zero;
        }
        bool leftHit = Physics2D.Raycast(rb.position + new Vector2(-2.575f, -0.875f / 2), Vector2.up, 0.875f, LayerMask.GetMask("LandTiles")).collider != null
            && Physics2D.Raycast(rb.position + new Vector2(-2.575f, -0.875f / 2), Vector2.up, 0.875f, LayerMask.GetMask("WaterTiles")).collider == null;
        boatWallLeft.enabled = !canLand || !leftHit;
        bool rightHit = Physics2D.Raycast(rb.position + new Vector2(2.575f, -0.875f / 2), Vector2.up, 0.875f, LayerMask.GetMask("LandTiles")).collider != null
            && Physics2D.Raycast(rb.position + new Vector2(2.575f, -0.875f / 2), Vector2.up, 0.875f, LayerMask.GetMask("WaterTiles")).collider == null;
        boatWallRight.enabled = !canLand || !rightHit;
        bool topHit = Physics2D.Raycast(rb.position + new Vector2(-1.625f / 2, 2.2f), Vector2.right, 1.625f, LayerMask.GetMask("LandTiles")).collider != null
            && Physics2D.Raycast(rb.position + new Vector2(-1.625f / 2, 2.2f), Vector2.right, 1.625f, LayerMask.GetMask("WaterTiles")).collider == null;
        boatWallTop.enabled = !canLand || !topHit;
        bool bottomHit = Physics2D.Raycast(rb.position + new Vector2(-1.625f / 2, -2.2f), Vector2.right, 1.625f, LayerMask.GetMask("LandTiles")).collider != null
            && Physics2D.Raycast(rb.position + new Vector2(-1.625f / 2, -2.2f), Vector2.right, 1.625f, LayerMask.GetMask("WaterTiles")).collider == null;
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
        GameObject other = collision.gameObject;
        LaserButton laserButton = other.GetComponent<LaserButton>();
        if (laserButton != null)
        {
            laserButton.Enter();
        }
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            Piranha piranha = other.GetComponent<Piranha>();
            if (piranha != null)
            {
                Destroy(other);
            }
            Jellyfish jellyfish = other.GetComponent<Jellyfish>();
            if (jellyfish != null)
            {
                jellyfish.HitDelay();
            }
            Damage();
        }
        Coin coin = other.GetComponent<Coin>();
        if (coin != null)
        {
            player.CollectCoin(coin);
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

    private void Damage()
    {
        if (smashed || invincible) return;

        hitSound.Play();
        health--;
        SetTempInvincible();
        for (int i = 0; i < maxHealth; i++)
        {
            emptyHearts[i].SetActive(i >= health);
        }
        ShowHearts();
        if (health <= 0)
        {
            smashed = true;
            unsmashedSprite = sr.sprite;
            sr.sprite = smashedSprite;
            boatRestorePrompt.SetActive(true);
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    public void ShowMaze()
    {
        Maze maze = Instantiate(mazePrefab, hudCanvas).GetComponent<Maze>();
        maze.boat = this;
    }

    public void Restore()
    {
        smashed = false;
        sr.sprite = unsmashedSprite;
        boatRestorePrompt.SetActive(false);
        health = maxHealth;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        HideHearts();
        for (int i = 0; i < maxHealth; i++)
        {
            emptyHearts[i].SetActive(false);
        }
        SetTempInvincible();
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

    public void SetTempInvincible()
    {
        invincible = true;
        sr.color = partialTransparent;
        StartCoroutine(Invincibility());
    }

    private IEnumerator Invincibility()
    {
        yield return new WaitForSeconds(invincibleTime);
        sr.color = Color.white;
        invincible = false;
    }
}
