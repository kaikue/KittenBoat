using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Dialog dialog;
    protected HashSet<string> flags;

    protected virtual void Start()
    {
        flags = new HashSet<string>();
    }

    public virtual void Interact(Player player)
    {
        OpenDialog(dialog, player);
    }

    protected void OpenDialog(Dialog d, Player player)
    {
        Dialog newDialog = Instantiate(d).GetComponent<Dialog>();
        newDialog.player = player;
        newDialog.npc = this;
    }

    public virtual void SetFlag(string flag)
    {
        flags.Add(flag);
    }

    public virtual void DoAction(string action)
    {
        print("TODO: implement " + action);
    }
}
