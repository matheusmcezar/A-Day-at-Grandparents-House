using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public GameObject controlsModal;
    public Color loadToColor;
    public float fadeSpeed = 2f;

    public void StartGame()
    {
        Initiate.Fade("Outside", loadToColor, fadeSpeed);
    }

    public void ShowControls()
    {
        this.controlsModal.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
