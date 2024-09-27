using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public Item item = null;
    
    public void addItem(Item item) {
        this.item = item;
        this.item.slot = this;
        this.GetComponent<Image>().sprite = this.item.sprite;
    }
}
