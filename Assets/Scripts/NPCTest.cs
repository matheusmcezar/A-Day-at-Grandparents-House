using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTest : MonoBehaviour, IActionable
{
    public void StartAction()
    {
        DialogBoxPayload payload = new DialogBoxPayload(
            "Grandpa Tutuca",
            "grandpa_tutuca",
            "npc_tutuca_text"
        );
        GameManager.instance.showDialogBox(payload);
    }
}
