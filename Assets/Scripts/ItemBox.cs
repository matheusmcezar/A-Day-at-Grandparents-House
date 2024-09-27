using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour, IActionable
{
    public bool isClosed = true;
    public Sprite openSprite;
    public Item content;
    public Inventory playerInventory;

    public void StartAction()
    {
        if (this.isClosed) {
            this.isClosed = false;
            this.GetComponent<SpriteRenderer>().sprite = this.openSprite;
            playerInventory.addItem(this.content);
        }
    }
}
