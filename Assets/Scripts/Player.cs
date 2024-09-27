using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private KeyCode KC_LEFT;
    private KeyCode KC_RIGHT;
    private KeyCode KC_UP;
    private KeyCode KC_DOWN;
    private KeyCode KC_ACTION;
    private KeyCode KC_INVENTORY;

    private BoxCollider2D playerBoxCollider;

    private float playerLocalScale;

    public float playerSpeed = 4f;
    public PlayerDirection playerDirection = PlayerDirection.DOWN;
    public bool playerIsWalking = false;
    public bool lockMovement = false;
    public bool playerCanInteract = true;

    public Animator playerAnimator;

    public Inventory inventory;

    void Start()
    {
        setKeyCodes();
        playerBoxCollider = GetComponent<BoxCollider2D>();
        playerLocalScale = transform.localScale.x;
    }

    void Update()
    {
        if (! this.lockMovement)
        {
            HandleMovement();
            HandleDirection();
            HandleAnimation();
            HandleAction();
        }
    }

    private void HandleMovement()
    {
        if (this.inventory.gameObject.activeInHierarchy) return;

        Transform playerTransform = GetComponent<Transform>();
        playerIsWalking = false;
        
        if (Input.GetKey(KC_LEFT) && playerCanMove(PlayerDirection.LEFT))
        {
            playerDirection = PlayerDirection.LEFT;
            playerIsWalking = true;
            transform.Translate(Vector3.left * playerSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KC_UP) && playerCanMove(PlayerDirection.UP))
        {
            playerDirection = PlayerDirection.UP;
            playerIsWalking = true;
            transform.Translate(Vector3.up * playerSpeed * Time.deltaTime);
            
        }
        if (Input.GetKey(KC_RIGHT) && playerCanMove(PlayerDirection.RIGHT))
        {
            playerDirection = PlayerDirection.RIGHT;
            playerIsWalking = true;
            transform.Translate(Vector3.right * playerSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KC_DOWN) && playerCanMove(PlayerDirection.DOWN))
        {
            playerDirection = PlayerDirection.DOWN;
            playerIsWalking = true;
            transform.Translate(Vector3.down * playerSpeed * Time.deltaTime);
            
        }
    }

    private void HandleAction()
    {
        if (Input.GetKeyDown(KC_ACTION) && playerCanInteract)
        {
            GameObject target = this.sendRayCast(this.playerDirection, 0.2f);

            if (target != null) {
                Component component = target.GetComponent("IActionable");
                IActionable action = component as IActionable;
                if (action != null)
                {
                    action.StartAction();
                }
            }
        }

        if (Input.GetKeyDown(KC_INVENTORY))
        {
            GameObject inventoryGO = this.inventory.gameObject;
            playerCanInteract = inventoryGO.activeInHierarchy;
            inventoryGO.SetActive(!inventoryGO.activeInHierarchy);
        }
    }

    private void HandleDirection()
    {
        if (playerDirection == PlayerDirection.LEFT) {
            transform.localScale = new Vector3(-playerLocalScale, playerLocalScale, playerLocalScale);
        } else {
            transform.localScale = new Vector3(playerLocalScale, playerLocalScale, playerLocalScale);
        }
    }

    private void HandleAnimation()
    {
        if (playerIsWalking) {
            playerAnimator.SetBool("playerIsWalking", true);
        } else {
            playerAnimator.SetBool("playerIsWalking", false);
        }
        switch (playerDirection) {
            case PlayerDirection.UP:
                playerAnimator.SetInteger("playerDirection", 1);
                break;
            case PlayerDirection.DOWN:
                playerAnimator.SetInteger("playerDirection", 2);
                break;
            default:
                playerAnimator.SetInteger("playerDirection", 3);
                break;                    
        }
    }

    public enum PlayerDirection
    {
        LEFT, RIGHT, UP, DOWN
    }

    private bool playerCanMove(PlayerDirection direction) {
        return this.sendRayCast(direction) == null;
    }

    private void setKeyCodes()
    {
        KC_LEFT = KeyCode.LeftArrow;
        KC_RIGHT = KeyCode.RightArrow;
        KC_UP = KeyCode.UpArrow;
        KC_DOWN = KeyCode.DownArrow;
        KC_ACTION = KeyCode.Z;
        KC_INVENTORY = KeyCode.X;
    }

    private GameObject sendRayCast(PlayerDirection direction, float rayCastDistance = 0.05f)
    {
        Vector2 rayCastCenter = new Vector2(transform.position.x, transform.position.y + (playerBoxCollider.offset.y * transform.localScale.y));

        RaycastHit2D hit = new RaycastHit2D();

        if (direction == PlayerDirection.RIGHT) {
            hit = Physics2D.BoxCast(rayCastCenter, playerBoxCollider.size * transform.localScale, 0f, transform.right, rayCastDistance, LayerMask.GetMask("Default"));
        }
        if (direction == PlayerDirection.LEFT) {
            if (playerDirection == PlayerDirection.LEFT) {
                hit = Physics2D.BoxCast(rayCastCenter, playerBoxCollider.size * new Vector3(playerLocalScale, playerLocalScale, playerLocalScale), 0f, transform.right * -1, rayCastDistance);
            } else {
                hit = Physics2D.BoxCast(rayCastCenter, playerBoxCollider.size * transform.localScale, 0f, transform.right * -1, rayCastDistance);
            }
        }
        if (direction == PlayerDirection.UP) {
            hit = Physics2D.BoxCast(rayCastCenter, playerBoxCollider.size * transform.localScale, 0f, transform.up, rayCastDistance);
        }
        if (direction == PlayerDirection.DOWN) {
            hit = Physics2D.BoxCast(rayCastCenter, playerBoxCollider.size * transform.localScale, 0f, transform.up * -1, rayCastDistance);
        }
        
        if (hit.collider != null) {
            return hit.collider.gameObject;
        }
        return null;
    }
}
