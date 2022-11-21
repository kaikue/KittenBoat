using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Dialog : MonoBehaviour
{
    [System.Serializable]
    public class DialogLine
	{
        public string text;
        public UnityEvent<object> lineEvent;
        public object lineEventArg;
    }

    private int currentLine = 0;
    public TextMeshProUGUI textObj;
    [HideInInspector]
    public Player player;

    public DialogLine[] dialogLines;

    private void Awake()
    {
        UpdateText();
    }

    private void Start()
    {
        if (player != null)
        {
            player.StartTalking();
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interact"))
		{
            AdvanceText();
		}
    }

    private void AdvanceText()
    {
        if (dialogLines[currentLine].lineEvent != null)
        {
            dialogLines[currentLine].lineEvent.Invoke(dialogLines[currentLine].lineEventArg);
        }
        currentLine++;
        if (currentLine >= dialogLines.Length)
		{
            if (player != null)
            {
                player.StopTalking();
            }
            Destroy(gameObject);
            return;
		}
        UpdateText();
    }

    private void UpdateText()
	{
        textObj.text = dialogLines[currentLine].text;
    }

    public void TestAction(string text)
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
