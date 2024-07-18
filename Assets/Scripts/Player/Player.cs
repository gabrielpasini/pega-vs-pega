using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    public float jumpForce;
    public float wallJumpForce;
    public bool isOnGround;
    public bool isOnWall;
    private bool canJump;
    private bool canPress = true;
    private float wallJumpingTimer = 0;
    public float wallJumpingTimeout;
    public float actualJump = 0;
    public float maxJumps;

    public float speed;
    void Start() {
        canJump = true;
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update() {
        float moveX = Input.GetAxisRaw("Horizontal_WASD");
        Flip(moveX);
        if (Input.GetKeyDown(KeyCode.W) && canPress) {
            canPress = false;
            StartCoroutine(JumpDebounce());
            if (canJump && actualJump < maxJumps) {
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                actualJump++;
                if (isOnWall && !isOnGround) {
                    rb.AddForce(new Vector2((moveX * -1) * wallJumpForce, wallJumpForce));
                    wallJumpingTimer = 0f;
                } else {
                    rb.AddForce(new Vector2(0f, jumpForce));
                }
            } else {
                canJump = false;
            }
        }

        wallJumpingTimer += Time.deltaTime;
        if (wallJumpingTimer > wallJumpingTimeout) {
            rb.velocity = new Vector2(moveX * speed, rb.velocity.y);
        }
    }

    public IEnumerator JumpDebounce() {
        yield return new WaitForSeconds(0.05f);
        canPress = true;
    }

    private void Flip(float horizontal) {
        if (horizontal > 0) {
            sprite.flipX = false;
        } else if (horizontal < 0) {
            sprite.flipX = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "CanJump") {
            actualJump = 0;
            canJump = true;
            isOnGround = true;
        }
        if (other.gameObject.tag == "WallJump") {
            isOnWall = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.tag == "CanJump") {
            isOnGround = false;
        }
        if (other.gameObject.tag == "WallJump") {
            isOnWall = false;
        }
    }
}
