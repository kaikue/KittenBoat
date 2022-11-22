using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WearableItem : MonoBehaviour
{
    public Sprite altSprite;
    private Sprite sprite;
    private Player player;
    private SpriteRenderer sr;
    private SpriteRenderer playerSR;
    public GameObject obtainedDialog;
    public string obtainedMessage;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sprite = sr.sprite;
        player = FindObjectOfType<Player>();
        playerSR = player.gameObject.GetComponent<SpriteRenderer>();
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;
        Dialog newDialog = Instantiate(obtainedDialog).GetComponent<Dialog>();
        newDialog.dialogLines[0].text = obtainedMessage;
        newDialog.UpdateText();
        newDialog.player = player;
    }

    private void LateUpdate()
    {
        sr.flipX = playerSR.flipX;
        if (altSprite)
        {
            sr.sprite = player.walkSpriteUsed ? altSprite : sprite;
        }
    }
}
