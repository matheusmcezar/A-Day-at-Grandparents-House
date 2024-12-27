using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class MessageBox : MonoBehaviour
{
    [SerializeField]
    private Player player;

    public TextMeshProUGUI textMeshComponent;
    public KeyCode actionKeyCode;
    public string message;
    
    private void Awake  ()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        this.player = playerObject.GetComponent<Player>();
    }

    void OnEnable()
    {
        player.lockMovement = true;
        textMeshComponent.text = message;
    }

    void OnDisable()
    {
        textMeshComponent.text = string.Empty;
        player.lockMovement = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(actionKeyCode)) {
            gameObject.SetActive(false);
        }
    }
}
