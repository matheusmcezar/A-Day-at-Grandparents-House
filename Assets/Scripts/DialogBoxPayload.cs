using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogBoxPayload
{
    // Start is called before the first frame update
    public string whoIsTalking;
    public string imagePath;
    public string fileName;

    public DialogBoxPayload(string whoIsTalking, string imagePath, string fileName)
    {
        this.whoIsTalking = whoIsTalking;
        this.imagePath = imagePath;
        this.fileName = fileName;
    }
}
