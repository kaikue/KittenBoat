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
        public UnityEvent<string> lineEvent;
    }

    private int currentLine = 0;
    public TextMeshProUGUI textObj;
    [HideInInspector]
    public Player player;
    [HideInInspector]
    public NPC npc;

    public DialogLine[] dialogLines;

    private AudioSource sfx;
    public AudioClip openSound;
    public AudioClip advanceSound;

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
        sfx = GetComponent<AudioSource>();
        if (FindObjectsOfType<Dialog>().Length + FindObjectsOfType<DialogChoice>().Length <= 1)
        {
            sfx.PlayOneShot(openSound);
        }
        else
        {
            sfx.PlayOneShot(advanceSound);
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
            dialogLines[currentLine].lineEvent.Invoke(""); //gets overwritten by editor input
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
        sfx.PlayOneShot(advanceSound);
        UpdateText();
    }

    public void UpdateText()
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
