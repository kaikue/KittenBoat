using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Sprite standSprite;
    public Sprite walkSprite;

    private SpriteRenderer sr;
    private const float walkTime = 0.2f;
    private float walkTimer = 0;
    private bool walkSpriteUsed = false;
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

    private void Start() {
        sr = GetComponent<SpriteRenderer>();
        standSprite = sr.sprite;
        rb = GetComponent<Rigidbody2D>();
        cameraZoomedInSize = Camera.main.orthographicSize;
        cameraZoomedOutSize = cameraZoomedInSize * cameraZoomFactor;
        musicManager = FindObjectOfType<MusicManager>();
    }

    private void Update() {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");
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

        if (inputX < 0)
        {
            sr.flipX = true;
        }
        else if (inputX > 0)
        {
            sr.flipX = false;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(inputX, inputY) * walkSpeed;
        if (transform.parent != null && transform.parent.GetComponent<Rigidbody2D>() != null)
        {
            rb.velocity += transform.parent.GetComponent<Rigidbody2D>().velocity;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;
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
            musicManager.SetMusic(musicManager.mainIslandMusicSrc);
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
}
