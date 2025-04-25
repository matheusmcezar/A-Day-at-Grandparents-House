using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapExit : MonoBehaviour, IActionable
{
    public void StartAction()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        Player player = playerObject.GetComponent<Player>();
        player.playerAnimator.SetBool("playerIsWalking", false);
        player.playerDirection = Vector2.up;
        playerObject.transform.Translate(Vector3.up * player.playerSpeed * 5 * Time.deltaTime);
        GameManager.instance.showMessageBox("Ainda não posso ir embora...");
    }
}
