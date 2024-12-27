using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class DialogBox : MonoBehaviour
{
    public Player player;

    public TextMeshProUGUI textMeshComponent;
    public TextMeshProUGUI whoIsTalkingComponent;
    public Image whoIsTalkingImageComponent;
    
    public float textSpeed;
    public KeyCode actionKeyCode;
    public string dialogFileName;
    public string whoIsTalking;
    public string imagePath;

    private string[] textLines;
    private int linesIndex;
    private AudioManager audioManager;

    void Awake()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        this.player = playerObject.GetComponent<Player>();
        this.audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void OnEnable()
    {
        player.lockMovement = true;

        whoIsTalkingComponent.text = whoIsTalking;

        Sprite sprite = Resources.Load<Sprite>(imagePath);
        if (sprite != null) {
            whoIsTalkingImageComponent.sprite = sprite;
        }

        textMeshComponent.text = string.Empty;
        textLines = GetTextLines();
        linesIndex = 0;
        StartDialog();
    }

    void OnDisable()
    {
        player.lockMovement = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(actionKeyCode)) {
            if (textMeshComponent.text == textLines[linesIndex]) {
                NextLine();
            } else {
                FinishLine();
            }
        }
    }

    string[] GetTextLines()
    {
        return File.ReadAllLines(Application.dataPath + "/Texts/" + dialogFileName);
    }

    void StartDialog()
    {
        audioManager.PlayDialogSFX(audioManager.dialogWrite);
        linesIndex = 0;
        StartCoroutine(PrintLine());
    }

    void FinishLine()
    {
        StopAllCoroutines();
        textMeshComponent.text = textLines[linesIndex];
        audioManager.StopDialogSFX();
    }

    void NextLine()
    {
        if (linesIndex < textLines.Length - 1) {
            linesIndex++;
            textMeshComponent.text = string.Empty;
            audioManager.PlayDialogSFX(audioManager.dialogWrite);
            StartCoroutine(PrintLine());
        } else {
            audioManager.StopDialogSFX();
            gameObject.SetActive(false);
        }
    }

    IEnumerator PrintLine()
    {
        foreach (char c in textLines[linesIndex].ToCharArray()) {
            textMeshComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        audioManager.StopDialogSFX();
    }
}
