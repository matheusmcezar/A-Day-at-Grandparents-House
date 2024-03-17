using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private KeyCode KC_LEFT;
    private KeyCode KC_RIGHT;
    private KeyCode KC_UP;
    private KeyCode KC_DOWN;

    private BoxCollider2D playerBoxCollider;

    private float playerLocalScale;

    public float playerSpeed = 4f;
    public PlayerDirection playerDirection = PlayerDirection.DOWN;
    public bool playerIsWalking = false;

    public PlayerAnimation playerAnimation;

    void Start()
    {
        setKeyCodes();
        playerBoxCollider = GetComponent<BoxCollider2D>();
        playerLocalScale = transform.localScale.x;
    }

    void Update()
    {
        HandleMovement();
        HandleDirection();
        HandleAnimation();
    }

    private void HandleMovement()
    {
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
            switch (playerDirection) {
                case PlayerDirection.UP:
                    playerAnimation.PlayAnimation("PlayerWalkUp");
                    break;
                case PlayerDirection.DOWN:
                    playerAnimation.PlayAnimation("PlayerWalkDown");
                    break;
                default:
                    playerAnimation.PlayAnimation("PlayerWalkRight");
                    break;                    
            }
        } else {
            switch (playerDirection) {
                case PlayerDirection.UP:
                    playerAnimation.PlayAnimation("PlayerIdleUp");
                    break;
                case PlayerDirection.DOWN:
                    playerAnimation.PlayAnimation("PlayerIdleDown");
                    break;
                default:
                    playerAnimation.PlayAnimation("PlayerIdleRight");
                    break;
            }
        }
    }

    public enum PlayerDirection
    {
        LEFT, RIGHT, UP, DOWN
    }

    private bool playerCanMove(PlayerDirection direction) {
        Vector2 rayCastCenter = new Vector2(transform.position.x, transform.position.y + (playerBoxCollider.offset.y * transform.localScale.y));
        float rayCastDistance = 0.05f;

        RaycastHit2D hit = new RaycastHit2D();

        if (direction == PlayerDirection.RIGHT) {
            hit = Physics2D.BoxCast(rayCastCenter, playerBoxCollider.size * transform.localScale, 0f, transform.right, rayCastDistance, LayerMask.GetMask("Default"));
        }
        if (direction == PlayerDirection.LEFT) {
            hit = Physics2D.BoxCast(rayCastCenter, playerBoxCollider.size * transform.localScale, 0f, transform.right * -1, rayCastDistance);
        }
        if (direction == PlayerDirection.UP) {
            hit = Physics2D.BoxCast(rayCastCenter, playerBoxCollider.size * transform.localScale, 0f, transform.up, rayCastDistance);
        }
        if (direction == PlayerDirection.DOWN) {
            hit = Physics2D.BoxCast(rayCastCenter, playerBoxCollider.size * transform.localScale, 0f, transform.up * -1, rayCastDistance);
        }
        
        if (hit.collider != null) {
            Debug.Log(hit.collider.gameObject);
            Debug.DrawLine(rayCastCenter, hit.collider.gameObject.transform.position);
            return false;
        }

        return true;
    }

    private void setKeyCodes()
    {
        KC_LEFT = KeyCode.LeftArrow;
        KC_RIGHT = KeyCode.RightArrow;
        KC_UP = KeyCode.UpArrow;
        KC_DOWN = KeyCode.DownArrow;
    }
}
