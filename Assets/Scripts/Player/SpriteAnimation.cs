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
        if (this.player.isSpinning && !this.player.isBellying && !this.player.isSliding) {
            this.animator.SetBool("PlayerSpinning", true);
        } else {
            this.animator.SetBool("PlayerSpinning", false);
        }
        if (this.player.isOnGround) {
            float speedX = this.rigidbody.velocity.x;
            if (!this.player.isBellying) {
                this.animator.SetBool("PlayerBellying", false);
            }
            if (speedX != 0) {
                if (this.player.isSliding) {
                    this.animator.SetBool("PlayerSliding", true);
                    this.animator.SetBool("PlayerRunning", false);
                } else {
                    this.animator.SetBool("PlayerSliding", false);
                    this.animator.SetBool("PlayerRunning", true);
                }
            } else {
                this.animator.SetBool("PlayerRunning", false);
                this.animator.SetBool("PlayerSliding", false);
            }
            this.animator.SetBool("PlayerJumping", false);
            this.animator.SetBool("PlayerDoubleJumping", false);
            this.animator.SetBool("PlayerFalling", false);
        } else {
            float speedY = this.rigidbody.velocity.y;
            if (this.player.isBellying) {
                this.animator.SetBool("PlayerBellying", true);
                this.animator.SetBool("PlayerFalling", false);
                this.animator.SetBool("PlayerJumping", false);
                this.animator.SetBool("PlayerDoubleJumping", false);
            } else {
                if (speedY > 0) {
                    if (this.player.actualJump == this.player.maxJumps) {
                        this.animator.SetBool("PlayerDoubleJumping", true);
                    } else {
                        this.animator.SetBool("PlayerJumping", true);
                    }
                    this.animator.SetBool("PlayerFalling", false);
                    this.animator.SetBool("PlayerSliding", false);
                } else if (speedY < 0) {
                    this.animator.SetBool("PlayerFalling", true);
                    this.animator.SetBool("PlayerJumping", false);
                    this.animator.SetBool("PlayerDoubleJumping", false);
                }
            }
            this.animator.SetBool("PlayerRunning", false);
        }
    }
}
