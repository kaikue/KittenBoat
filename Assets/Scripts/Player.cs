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

    private Coroutine crtCameraZoom;
    private float cameraZoomedOutSize;
    private float cameraZoomedInSize;
    private const float cameraZoomTime = 1;
    private const float cameraZoomFactor = 2;

    private void Start() {
        sr = GetComponent<SpriteRenderer>();
        standSprite = sr.sprite;
        rb = GetComponent<Rigidbody2D>();
        cameraZoomedInSize = Camera.main.orthographicSize;
        cameraZoomedOutSize = cameraZoomedInSize * cameraZoomFactor;
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
            TryStopCoroutine(crtCameraZoom);
            crtCameraZoom = StartCoroutine(ZoomCamera(cameraZoomedOutSize));
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
            TryStopCoroutine(crtCameraZoom);
            crtCameraZoom = StartCoroutine(ZoomCamera(cameraZoomedInSize));
        }
    }

    private void TryStopCoroutine(Coroutine crt)
    {
        if (crt != null)
        {
            StopCoroutine(crt);
        }
    }

    private IEnumerator ZoomCamera(float newSize)
    {
        float oldSize = Camera.main.orthographicSize;
        for (float t = 0; t < cameraZoomTime; t += Time.deltaTime)
        {
            Camera.main.orthographicSize = Mathf.Lerp(oldSize, newSize, t / cameraZoomTime);
            yield return null;
        }
        Camera.main.orthographicSize = newSize;
    }
}
