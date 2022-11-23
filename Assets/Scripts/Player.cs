using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Sprite standSprite;
    public Sprite walkSprite;

    private SpriteRenderer sr;
    private const float walkTime = 0.2f;
    private float walkTimer = 0;
    [HideInInspector]
    public bool walkSpriteUsed = false;
    private const float walkSpeed = 3f;

    private float inputX;
    private float inputY;

    private Rigidbody2D rb;

    private Coroutine crtTransition;
    private float cameraZoomedOutSize;
    private float cameraZoomedInSize;
    private const float transitionTime = 1;
    private const float cameraZoomFactor = 2;

    private MusicManager musicManager;
    private bool hubMusicZone;
    private bool deepwaterMusicZone;

    private Coroutine crtPush;
    private const float pushTime = 0.3f;

    [HideInInspector]
    public int coins = 0;
    public TextMeshProUGUI coinsText;
    private Coroutine crtShowCoins;
    public RectTransform coinsTransform;

    private NPC interactableNPC;
    private bool talking;

    private AudioSource sfx;
    public AudioClip sfxCoin;
    public AudioClip sfxPush;
    public AudioClip sfxDialogClose;

    private void Start() {
        sr = GetComponent<SpriteRenderer>();
        standSprite = sr.sprite;
        rb = GetComponent<Rigidbody2D>();
        cameraZoomedInSize = Camera.main.orthographicSize;
        cameraZoomedOutSize = cameraZoomedInSize * cameraZoomFactor;
        musicManager = FindObjectOfType<MusicManager>();
        sfx = GetComponent<AudioSource>();
    }

    private void Update() {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");
        if (talking)
        {
            sr.sprite = walkSprite;
        }
        else
        {
            if (inputX != 0 || inputY != 0)
            {
                walkTimer += Time.deltaTime;
                if (walkTimer > walkTime)
                {
                    walkTimer = 0;
                    walkSpriteUsed = !walkSpriteUsed;
                    sr.sprite = walkSpriteUsed ? walkSprite : standSprite;
                }
            }
            else
            {
                sr.sprite = walkSprite;
            }

            if (inputX < 0)
            {
                sr.flipX = true;
            }
            else if (inputX > 0)
            {
                sr.flipX = false;
            }

            if (Input.GetButtonDown("Interact"))
            {
                if (interactableNPC != null)
                {
                    interactableNPC.Interact(this);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (talking)
        {
            rb.velocity = Vector2.zero;
        }
        else
        {
            rb.velocity = new Vector2(inputX, inputY) * walkSpeed;
            if (transform.parent != null && transform.parent.GetComponent<Rigidbody2D>() != null)
            {
                rb.velocity += transform.parent.GetComponent<Rigidbody2D>().velocity;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;

        Pushable pushable = other.GetComponent<Pushable>();
        if (pushable != null)
        {
            TryStopCoroutine(crtPush);
            crtPush = StartCoroutine(Push(pushable, -collision.GetContact(0).normal));
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;

        Pushable pushable = other.GetComponent<Pushable>();
        if (pushable != null)
        {
            TryStopCoroutine(crtPush);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;

        MovePad movePad = other.GetComponent<MovePad>();
        if (movePad != null)
        {
            movePad.boat.rb.AddForce(movePad.direction * MovePad.force);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("BoatZone"))
        {
            gameObject.layer = LayerMask.NameToLayer("PlayerBoat");
            transform.parent = other.transform.parent;
        }
        if (other.CompareTag("BoatFreezer"))
        {
            other.GetComponentInParent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            TryStopCoroutine(crtTransition);
            crtTransition = StartCoroutine(Transition(cameraZoomedOutSize));
            musicManager.SetMusic(musicManager.boatMusicSrc);
        }
        if (other.CompareTag("HubZone"))
        {
            hubMusicZone = true;
        }
        if (other.CompareTag("DeepwaterZone"))
        {
            deepwaterMusicZone = true;
        }
        ButtonPad buttonPad = other.GetComponent<ButtonPad>();
        if (buttonPad != null)
        {
            buttonPad.Enter();
        }
        ResetButton resetButton = other.GetComponent<ResetButton>();
        if (resetButton != null)
        {
            resetButton.ResetPuzzle();
        }
        Coin coin = other.GetComponent<Coin>();
        if (coin != null)
        {
            AddCoins(coin.value);
            sfx.PlayOneShot(sfxCoin);
            Destroy(other);
        }
        NPC npc = other.GetComponent<NPC>();
        if (npc != null)
        {
            interactableNPC = npc;
        }
        Crab crab = other.GetComponent<Crab>();
        if (crab != null)
        {
            musicManager.StopMusicOverride();
            crab.Catch();
        }
        CaveCeiling caveCeiling = other.GetComponent<CaveCeiling>();
        if (caveCeiling != null)
        {
            caveCeiling.Enter();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("BoatZone"))
        {
            gameObject.layer = LayerMask.NameToLayer("Player");
            transform.parent = null;
        }
        if (other.CompareTag("BoatFreezer"))
        {
            other.GetComponentInParent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            TryStopCoroutine(crtTransition);
            crtTransition = StartCoroutine(Transition(cameraZoomedInSize));
            PlayLandMusic();
        }
        if (other.CompareTag("HubZone"))
        {
            hubMusicZone = false;
        }
        if (other.CompareTag("DeepwaterZone"))
        {
            deepwaterMusicZone = false;
        }
        ButtonPad buttonPad = other.GetComponent<ButtonPad>();
        if (buttonPad != null)
        {
            buttonPad.Exit();
        }
        NPC npc = other.GetComponent<NPC>();
        if (npc != null && interactableNPC == npc)
        {
            interactableNPC = null;
        }
        CaveCeiling caveCeiling = other.GetComponent<CaveCeiling>();
        if (caveCeiling != null)
        {
            caveCeiling.Exit();
        }
    }

    private void TryStopCoroutine(Coroutine crt)
    {
        if (crt != null)
        {
            StopCoroutine(crt);
        }
    }

    private IEnumerator Transition(float newCameraSize)
    {
        float oldSize = Camera.main.orthographicSize;
        for (float t = 0; t < transitionTime; t += Time.deltaTime)
        {
            Camera.main.orthographicSize = Mathf.Lerp(oldSize, newCameraSize, t / transitionTime);
            yield return null;
        }
        Camera.main.orthographicSize = newCameraSize;
    }

    private IEnumerator Push(Pushable pushable, Vector2 direction)
    {
        //keep checking until moved in same dir for uninterrupted pushTime
        float timer = pushTime;
        while (true)
        {
            if (timer <= 0)
            {
                pushable.PushDirection(direction);
                sfx.PlayOneShot(sfxPush);
                break;
            }
            Vector2 moveDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if (Vector2.Dot(moveDir, direction) > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                timer = pushTime;
            }
            yield return null;
        }
    }

    public void StartTalking()
    {
        talking = true;
    }

    public void StopTalking()
    {
        talking = false;
        if (FindObjectsOfType<Dialog>().Length + FindObjectsOfType<DialogChoice>().Length <= 1)
        {
            sfx.PlayOneShot(sfxDialogClose);
        }
    }

    public void AddCoins(int value)
    {
        coins += value;
        coinsText.text = Mathf.Clamp(coins, 0, 9999).ToString();
        TryStopCoroutine(crtShowCoins);
        crtShowCoins = StartCoroutine(ShowCoins(2));
    }

    public void ShowCoinsLong()
    {
        TryStopCoroutine(crtShowCoins);
        crtShowCoins = StartCoroutine(ShowCoins(999));
    }

    public void ShowCoinsShort()
    {
        TryStopCoroutine(crtShowCoins);
        crtShowCoins = StartCoroutine(ShowCoins(1));
    }

    private IEnumerator ShowCoins(float showTime)
    {
        int hideY = 260;
        int showY = 185;
        float startY = Mathf.Min(hideY, coinsTransform.anchoredPosition.y);
        float moveTime = 0.5f;

        for (float t = 0; t < moveTime; t += Time.deltaTime)
        {
            float y = Mathf.Lerp(startY, showY, t / moveTime);
            coinsTransform.anchoredPosition = new Vector2(coinsTransform.anchoredPosition.x, y);
            yield return null;
        }
        coinsTransform.anchoredPosition = new Vector2(coinsTransform.anchoredPosition.x, showY);
        yield return new WaitForSeconds(showTime);
        for (float t = 0; t < moveTime; t += Time.deltaTime)
        {
            float y = Mathf.Lerp(showY, hideY, t / moveTime);
            coinsTransform.anchoredPosition = new Vector2(coinsTransform.anchoredPosition.x, y);
            yield return null;
        }
        coinsTransform.anchoredPosition = new Vector2(coinsTransform.anchoredPosition.x, hideY);
    }

    private void PlayLandMusic()
    {
        if (deepwaterMusicZone)
        {
            musicManager.SetMusic(musicManager.deepIslandMusicSrc);
        }
        else if (hubMusicZone)
        {
            musicManager.SetMusic(musicManager.mainIslandMusicSrc);
        }
        else
        {
            musicManager.SetMusic(musicManager.puzzleIslandMusicSrc);
        }
    }
}
