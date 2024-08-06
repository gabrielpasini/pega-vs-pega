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

    const string PLAYER_IDLE = "Idle";
    const string PLAYER_RUN = "Run";
    const string PLAYER_JUMP = "Jump";
    const string PLAYER_DOUBLE_JUMP = "DoubleJump";
    const string PLAYER_FALL = "Fall";
    const string PLAYER_SPIN = "Spin";
    const string PLAYER_SLIDE = "Slide";
    const string PLAYER_BELLY = "Belly";

    void Update() {
        if (this.player.isSpinning && !this.player.isBellying && !this.player.isSliding) {
            this.animator.Play(PLAYER_SPIN);
        }
        if (this.player.isOnGround) {
            float speedX = this.rigidbody.velocity.x;
            if (speedX != 0) {
                if (this.player.isSliding) {
                    this.animator.Play(PLAYER_SLIDE);
                } else {
                    this.animator.Play(PLAYER_RUN);
                }
            } else if (!this.player.isBellying) {
                this.animator.Play(PLAYER_IDLE);
            }
        } else {
            float speedY = this.rigidbody.velocity.y;
            if (this.player.isBellying) {
                this.animator.Play(PLAYER_BELLY);
            } else {
                if (speedY > 0) {
                    if (this.player.actualJump == this.player.maxJumps) {
                        this.animator.Play(PLAYER_DOUBLE_JUMP);
                    } else {
                        this.animator.Play(PLAYER_JUMP);
                    }
                } else if (speedY < 0) {
                    this.animator.Play(PLAYER_FALL);
                }
            }
        }
    }
}
