using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsModal : MonoBehaviour
{
    public Button buttonAfterActive;
    public Button buttonBeforeActive;
    void OnEnable()
    {
        this.buttonAfterActive.Select();
    }

    void OnDisable()
    {
        this.buttonBeforeActive.Select();
    }
}
