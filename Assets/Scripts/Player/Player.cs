using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private Rigidbody2D rb;
    public Vector2 jumpForce;
    private bool canJump;
    public float speed;
    void Start() {
        canJump = true;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        float moveX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveX * speed, rb.velocity.y);
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
            if (canJump) {
                canJump = false;
                rb.AddForce(jumpForce);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "CanJump") {
            canJump = true;
        }
    }
}
