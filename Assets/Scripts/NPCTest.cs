using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTest : MonoBehaviour, IActionable
{
    public DialogBox dialogBox;
 
    public void StartAction()
    {
        // refact: mover para game manager
        dialogBox.whoIsTalking = "Grandpa Tutuca";
        dialogBox.dialogFileName = "npc_tutuca_text.txt";
        dialogBox.gameObject.SetActive(true);
    }
}
