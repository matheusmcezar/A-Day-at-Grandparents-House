using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class KeyMap
{
    [SerializeField]
    private List<KeyMapItem> keyMapItems = new List<KeyMapItem>();

    public void Add(KeyAction action, KeyCode code)
    {
        KeyMapItem item = new KeyMapItem();
        item.keyaction = action;
        item.keyCode = code;
        keyMapItems.Add(item);
    }

    public KeyCode GetKeyCode(KeyAction keyAction)
    {
        return keyMapItems.Find(item => item.keyaction == keyAction).keyCode;
    }
}

[Serializable]
public class KeyMapItem
{
    [SerializeField]
    public KeyAction keyaction;
    [SerializeField]
    public KeyCode keyCode;
}

public enum KeyAction {
    LEFT, RIGHT, UP, DOWN, ACTION, CANCEL, MENU
}
