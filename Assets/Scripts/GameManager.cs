using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool lockMovement = false;
    [SerializeField]
    private MessageBox messageBox;
    [SerializeField]
    private DialogBox dialogBox;
    public KeyMap keyMap;
    
    private void Awake() {
        if (instance == null) {
            instance = this;
            this.messageBox = (MessageBox) FindAnyObjectByType(typeof(MessageBox), FindObjectsInactive.Include);
            this.dialogBox = (DialogBox) FindAnyObjectByType(typeof(DialogBox), FindObjectsInactive.Include);
            this.startKeyMap();
        } else {
            Destroy(gameObject);
        }
    }

    public void showMessageBox(string message)
    {
        messageBox.message = message;
        messageBox.gameObject.SetActive(true);
    }

    public void showDialogBox(DialogBoxPayload dialogBoxPayload)
    {
        dialogBox.whoIsTalking = dialogBoxPayload.whoIsTalking;
        dialogBox.imagePath = dialogBoxPayload.imagePath;
        dialogBox.dialogFileName = dialogBoxPayload.fileName;
        dialogBox.gameObject.SetActive(true);
    }

    private void startKeyMap()
    {
        this.keyMap = new KeyMap();
        this.keyMap.Add(KeyAction.LEFT, KeyCode.A);
        this.keyMap.Add(KeyAction.UP, KeyCode.W);
        this.keyMap.Add(KeyAction.RIGHT, KeyCode.D);
        this.keyMap.Add(KeyAction.DOWN, KeyCode.S);
        this.keyMap.Add(KeyAction.ACTION, KeyCode.E);
        this.keyMap.Add(KeyAction.MENU, KeyCode.C);
    }
}
