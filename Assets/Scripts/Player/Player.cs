using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    public float jumpForce;
    public bool isOnGround;
    private bool canJump;
    private bool canPress = true;
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
            if (canJump && actualJump < maxJumps) {
                    actualJump++;
                    rb.velocity = new Vector2(rb.velocity.x, 0f);
                    rb.AddForce(new Vector2(0f, jumpForce));
            } else {
                canJump = false;
            }
        }

        rb.velocity = new Vector2(moveX * speed, rb.velocity.y);
    }

    public IEnumerator JumpDebounce() {
        canPress = false;
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
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.tag == "CanJump") {
            isOnGround = false;
        }
    }
}
