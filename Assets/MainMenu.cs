using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject controlsModal;

    public void StartGame()
    {
        SceneManager.LoadSceneAsync("Outside");
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
