using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeOut : MonoBehaviour
{
    public int sceneIndex;
    private void OnFadeOutComplete()
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
