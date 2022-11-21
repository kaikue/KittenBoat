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
        public UnityEvent<object> chosenEvent;
        public object chosenEventArg;
    }

    public string promptText;
    public TextMeshProUGUI textObj;
    public ChoiceSelection[] choices;
    public GameObject uiPointer;
    private bool wasHeld;
    private int selectedChoice;
    [HideInInspector]
    public Player player;

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
                choices[selectedChoice].chosenEvent.Invoke(choices[selectedChoice].chosenEventArg);
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
    }

    public void ShowDialogChoice(DialogChoice dialogChoice)
    {
        DialogChoice newDialogChoice = Instantiate(dialogChoice).GetComponent<DialogChoice>();
        newDialogChoice.player = player;
    }
}
