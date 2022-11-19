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
        public UnityEvent lineEvent; //optional, plays on end / when chosen
        public DialogLine[] choices;
    }

    private const float charWaitTime = 0.05f;

    private int currentLine = 0;
    private int posInLine = 0;
    public TextMeshProUGUI textObj;
    private bool progressing = true;
    private float charWaitTimer = 0;
    [HideInInspector]
    public Player player;

    public DialogLine[] dialogLines;

    private void Awake()
    {
        textObj.text = "";
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interact"))
		{
            if (progressing)
			{
                posInLine = dialogLines[currentLine].text.Length;
                UpdateText();
			}
            else
			{
                AdvanceText();
			}
            return;
		}

        if (progressing)
        {
            charWaitTimer += Time.deltaTime;
            if (charWaitTimer >= charWaitTime)
			{
                charWaitTimer = 0;
                posInLine++;
                UpdateText();
            }
        }
    }

    private void UpdateText()
	{
        string currentText = dialogLines[currentLine].text;
        if (posInLine >= currentText.Length)
        {
            posInLine = currentText.Length;
            progressing = false;
        }
        string shownText = currentText.Substring(0, posInLine);
        string hiddenText = currentText.Substring(posInLine, currentText.Length - posInLine);
        textObj.text = shownText + "<color=#00000000>" + hiddenText + "</color>";
    }

    private void AdvanceText()
    {
        charWaitTimer = 0;
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
        dialogLines[currentLine].lineEvent.Invoke();
        posInLine = 0;
        progressing = true;
        UpdateText();
    }

    public void TestAction()
    {
        print("test");
    }
}
