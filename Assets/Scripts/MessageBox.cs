using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using System.IO;

public class MessageBox : MonoBehaviour
{
    [SerializeField]
    private Player player;
    public InputActionReference inputAction;

    public TextMeshProUGUI textMeshComponent;
    public string message;
    
    private void Awake  ()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        this.player = playerObject.GetComponent<Player>();
    }

    void OnEnable()
    {
        inputAction.action.started += CloseBox;
        player.lockMovement = true;
        textMeshComponent.text = message;
    }

    void OnDisable()
    {
        inputAction.action.started -= CloseBox;
        textMeshComponent.text = string.Empty;
        player.lockMovement = false;
    }

    private void CloseBox(InputAction.CallbackContext obj)
    {
        gameObject.SetActive(false);
    }
}
