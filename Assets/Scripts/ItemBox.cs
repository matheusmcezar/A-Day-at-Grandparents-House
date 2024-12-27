using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour, IActionable
{
    public bool isClosed = true;
    public Sprite openSprite;
    public Item content;

    public void StartAction()
    {
        if (this.isClosed) {
            this.isClosed = false;
            this.GetComponent<SpriteRenderer>().sprite = this.openSprite;
            GameObject playerObject = GameObject.FindWithTag("Player");
            Player player = playerObject.GetComponent<Player>();
            player.inventory.addItem(this.content);
        }
    }
}
