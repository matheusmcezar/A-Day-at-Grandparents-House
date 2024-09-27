using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool lockMovement = false;
    public MessageBox messageBox;
    
    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    public void showMessageBox(string message)
    {
        messageBox.message = message;
        messageBox.gameObject.SetActive(true);
    }
}
