using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTest : MonoBehaviour, IActionable
{
    public int textCont = -1;
    public void StartAction()
    {
        if (++this.textCont > 3)
        {
            this.textCont = Random.Range(4, 11);
        }

        DialogBoxPayload payload = new DialogBoxPayload(
            "Grandpa Tutuca",
            "grandpa_tutuca",
            "npc_tutuca_text_" + this.textCont
        );
        GameManager.instance.showDialogBox(payload);
    }
}
