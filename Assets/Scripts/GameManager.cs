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
    
    private void Awake() {
        if (instance == null) {
            instance = this;
            this.messageBox = (MessageBox) FindAnyObjectByType(typeof(MessageBox), FindObjectsInactive.Include);
            this.dialogBox = (DialogBox) FindAnyObjectByType(typeof(DialogBox), FindObjectsInactive.Include);
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
        dialogBox.dialogFileName = dialogBoxPayload.fileName + ".txt";
        dialogBox.gameObject.SetActive(true);
    }
}
