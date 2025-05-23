using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private BoxCollider2D playerBoxCollider;

    private SpriteRenderer spriteRenderer;

    public float playerSpeed = 4f;
    public bool playerIsWalking = false;
    public bool lockMovement = false;
    public bool playerCanInteract = true;

    public Animator playerAnimator;

    public Inventory inventory;

    public Vector2 playerDirection;
    public InputActionReference inputMove;
    public InputActionReference inputAction;
    public InputActionReference inputInventory;
    public AudioManager audioManager;
    public AudioClip footstepSound;

    void Start()
    {
        playerBoxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (! this.lockMovement)
        {
            HandleMovement();
            HandleAnimation();
            HandleFootstepSound();
        }
    }

    void OnEnable()
    {
        inputAction.action.started += DoAct;
        inputInventory.action.started += OpenInventory;
    }
    void OnDisable()
    {
        inputAction.action.started -= DoAct;
        inputInventory.action.started -= OpenInventory;
    }

    private void HandleMovement()
    {
        if (this.inventory.gameObject.activeInHierarchy) return;

        Transform playerTransform = GetComponent<Transform>();
        playerIsWalking = false;

        Vector2 tempDirection = inputMove.action.ReadValue<Vector2>();

        if (tempDirection != Vector2.zero) {
            playerDirection = tempDirection;
        
            if (playerCanMove(playerDirection)) {
                playerIsWalking = true;
                transform.Translate(playerDirection * playerSpeed * Time.deltaTime);
            }
        }
    }

    private void DoAct(InputAction.CallbackContext obj)
    {
        if (playerCanInteract)
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
    }

    private void OpenInventory(InputAction.CallbackContext obj)
    {
        GameObject inventoryGO = this.inventory.gameObject;
        playerCanInteract = inventoryGO.activeInHierarchy;
        inventoryGO.SetActive(!inventoryGO.activeInHierarchy);
    }

    private void HandleAnimation()
    {
        if (playerIsWalking) {
            playerAnimator.SetBool("playerIsWalking", true);
        } else {
            playerAnimator.SetBool("playerIsWalking", false);
        }
        if (playerDirection == Vector2.up) {
            playerAnimator.SetInteger("playerDirection", 1);
        } else if (playerDirection == Vector2.down) {
            playerAnimator.SetInteger("playerDirection", 2);
        } else if (playerDirection == Vector2.zero) {
        } else {
            if (playerDirection.x < 0) {
                spriteRenderer.flipX = true;
            } else {
                spriteRenderer.flipX = false;
            }
            playerAnimator.SetInteger("playerDirection", 3);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Component component = other.gameObject.GetComponent("IActionable");
        IActionable action = component as IActionable;
        if (action != null) {
            action.StartAction();
        }
    }

    private bool playerCanMove(Vector2 direction, float rayCastDistance = 0.05f)
    {
        return sendRayCast(direction, rayCastDistance) == null;
    }

    private GameObject sendRayCast(Vector2 direction, float rayCastDistance)
    {
        Vector2 rayCastCenter = new Vector2(transform.position.x, transform.position.y + (playerBoxCollider.offset.y * transform.localScale.y));
        RaycastHit2D hit = Physics2D.BoxCast(rayCastCenter, playerBoxCollider.size * transform.localScale, 0f, direction, rayCastDistance, LayerMask.GetMask("Default"));

        if (hit.collider != null) {
            return hit.collider.gameObject;
        }
        return null;
    }

    private void HandleFootstepSound()
    {
        if (this.playerIsWalking)
        {
            this.audioManager.PlayDialogSFX(this.footstepSound, 1.5f);
        }
        else
        {
            this.audioManager.StopDialogSFX();
        }
        
    }
}
