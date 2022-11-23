using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGet : MonoBehaviour
{
    public GameObject obtainedDialog;
    public string obtainedMessage;

    protected virtual void Start()
    {
        Dialog newDialog = Instantiate(obtainedDialog).GetComponent<Dialog>();
        newDialog.dialogLines[0].text = obtainedMessage;
        newDialog.UpdateText();
        newDialog.player = FindObjectOfType<Player>();
    }
}
