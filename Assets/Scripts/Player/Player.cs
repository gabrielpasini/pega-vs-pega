using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    public float speed;
    public float slideSpeed;
    public float spinDuration;
    public float slideDuration;
    public float bellyStunDuration;
    public float jumpForce;
    public float jumpSlidingForce;
    public bool isSpinning;
    public bool isSliding;
    public bool isCrouching;
    public bool isBellying;
    public bool isOnGround;
    private bool canJump;
    private bool canPress = true;
    public float actualJump = 0;
    public float maxJumps;

    void Start() {
        canJump = true;
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update() {
        float moveX = Input.GetAxisRaw("Horizontal_WASD");
        Flip(moveX);
        if (Input.GetKeyDown(KeyCode.W) && !isBellying && canPress) {
            // PULO
            if (canJump && actualJump < maxJumps) {
                    actualJump++;
                    rb.velocity = new Vector2(rb.velocity.x, 0f);
                    if (isSliding) {
                        rb.AddForce(new Vector2(0f, jumpSlidingForce));
                    } else {
                        rb.AddForce(new Vector2(0f, jumpForce));
                    }
            } else {
                canJump = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.F) && !isSliding) {

            // SLIDE
            if (isOnGround && rb.velocity.x != 0) {
                isSliding = true;
                rb.AddForce(new Vector2(slideSpeed * moveX, 0f));
                StartCoroutine("stopSlide");
            }
            // BARRIGADA
            if (!isBellying && !isOnGround) {
                isBellying = true;
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                StartCoroutine("performBelly");
            }
        }
        if (Input.GetKey(KeyCode.F) && isOnGround) {
            // AGACHAR INICIO
            if (rb.velocity.x == 0) {
                isCrouching = true;
            }
        } else {
            // AGACHAR FIM
            isCrouching = false;
        }
        // GIRO
        if (Input.GetKeyDown(KeyCode.Q) && !isSpinning) {
            isSpinning = true;
            StartCoroutine("stopSpinning");
        }

        // MOVIMENTAÇÃO
        if (!isSliding && !isBellying) {
            if (isCrouching) {
                rb.velocity = new Vector2(moveX * speed / 2, rb.velocity.y);
            } else {
                rb.velocity = new Vector2(moveX * speed, rb.velocity.y);
            }
        }
    }

    private IEnumerator stopSlide() {
        yield return new WaitForSeconds(slideDuration);
        isSliding = false;
    }
    private IEnumerator stopSpinning() {
        yield return new WaitForSeconds(spinDuration);
        isSpinning = false;
    }
    private IEnumerator performBelly() {
        rb.velocity = new Vector2(0f, rb.velocity.y);
        rb.AddForce(new Vector2(0f, jumpForce * 0.5f));
        yield return new WaitForSeconds(0.2f);
        rb.AddForce(new Vector2(0f, -jumpForce));
    }

    private IEnumerator stopBelly() {
        yield return new WaitForSeconds(bellyStunDuration);
        isBellying = false;
    }



    private IEnumerator JumpDebounce() {
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
            if (isBellying) {
                StartCoroutine("stopBelly");
            }

        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.tag == "CanJump") {
            isOnGround = false;
        }
    }
}
