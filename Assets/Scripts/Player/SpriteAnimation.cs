using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimation : MonoBehaviour {
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Rigidbody2D rigidbody;
    [SerializeField]
    private Player player;

    void Update() {
        float speedX = this.rigidbody.velocity.x;
        if (this.player.isOnGround) {
            if (speedX != 0) {
                this.animator.SetBool("PlayerRunning", true);
            } else {
                this.animator.SetBool("PlayerRunning", false);
            }
            this.animator.SetBool("PlayerJumping", false);
            this.animator.SetBool("PlayerDoubleJumping", false);
            this.animator.SetBool("PlayerFalling", false);
            this.animator.SetBool("PlayerWallJumping", false);
        } else {
            float speedY = this.rigidbody.velocity.y;
            if (speedY > 0) {
                if (this.player.actualJump == this.player.maxJumps) {
                    this.animator.SetBool("PlayerDoubleJumping", true);
                } else {
                    this.animator.SetBool("PlayerJumping", true);
                }
                this.animator.SetBool("PlayerFalling", false);
            } else if (speedY < 0) {
                this.animator.SetBool("PlayerJumping", false);
                this.animator.SetBool("PlayerDoubleJumping", false);
                this.animator.SetBool("PlayerFalling", true);
            }
            if (this.player.isOnWall) {
                this.animator.SetBool("PlayerWallJumping", true);
            } else {
                this.animator.SetBool("PlayerWallJumping", false);
            }
        }
    }
}
