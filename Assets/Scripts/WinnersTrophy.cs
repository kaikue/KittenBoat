using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinnersTrophy : MonoBehaviour
{
    public GameObject obtainedDialog;

    private void Start()
    {
        Player player = FindObjectOfType<Player>();
        Dialog newDialog = Instantiate(obtainedDialog).GetComponent<Dialog>();
        newDialog.player = player;
        player.hasTrophy = true;
    }
}
