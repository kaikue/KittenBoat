using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DialogChoice : MonoBehaviour
{
    [System.Serializable]
    public class ChoiceSelection
    {
        public GameObject ui;
        public string text;
        public UnityEvent<string> chosenEvent;
    }

    public string promptText;
    public TextMeshProUGUI textObj;
    public ChoiceSelection[] choices;
    public GameObject uiPointer;
    private bool wasHeld;
    private int selectedChoice;
    [HideInInspector]
    public Player player;
    [HideInInspector]
    public NPC npc;

    private void Start()
    {
        if (player != null)
        {
            player.StartTalking();
        }
        textObj.text = promptText;
        foreach (ChoiceSelection choice in choices)
        {
            choice.ui.GetComponentInChildren<TextMeshProUGUI>().text = choice.text;
        }
    }

    private void Update()
    {
        if (Input.GetAxis("Vertical") != 0)
        {
            if (!wasHeld)
            {
                wasHeld = true;
                bool up = Input.GetAxis("Vertical") > 0;
                if (up)
                {
                    selectedChoice--;
                    if (selectedChoice < 0)
                    {
                        selectedChoice += choices.Length;
                    }
                }
                else
                {
                    selectedChoice++;
                    if (selectedChoice >= choices.Length)
                    {
                        selectedChoice -= choices.Length;
                    }
                }
                uiPointer.transform.position = choices[selectedChoice].ui.transform.position;
            }
        }
        else
        {
            wasHeld = false;
        }

        if (Input.GetButtonDown("Interact"))
        {
            if (choices[selectedChoice].chosenEvent != null)
            {
                choices[selectedChoice].chosenEvent.Invoke(""); //gets overwritten by editor input
            }
            if (player != null)
            {
                player.StopTalking();
            }
            Destroy(gameObject);
        }
    }

    public void TestChoiceAction(string text)
    {
        print(text);
    }

    public void ShowDialog(Dialog dialog)
    {
        Dialog newDialog = Instantiate(dialog).GetComponent<Dialog>();
        newDialog.player = player;
        newDialog.npc = npc;
    }

    public void ShowDialogChoice(DialogChoice dialogChoice)
    {
        DialogChoice newDialogChoice = Instantiate(dialogChoice).GetComponent<DialogChoice>();
        newDialogChoice.player = player;
        newDialogChoice.npc = npc;
    }

    public void SetNPCFlag(string flag)
    {
        npc.SetFlag(flag);
    }

    public void DoNPCAction(string action)
    {
        npc.DoAction(action);
    }

    public void ShowPlayerCoins()
    {
        player.ShowCoinsLong();
    }

    public void HidePlayerCoins()
    {
        player.ShowCoinsShort();
    }
}
