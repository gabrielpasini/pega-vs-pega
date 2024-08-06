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
    private string currentState;

    const string PLAYER_IDLE = "Idle";
    const string PLAYER_RUN = "Run";
    const string PLAYER_JUMP = "Jump";
    const string PLAYER_DOUBLE_JUMP = "DoubleJump";
    const string PLAYER_FALL = "Fall";
    const string PLAYER_SPIN = "Spin";
    const string PLAYER_SLIDE = "Slide";
    const string PLAYER_BELLY = "Belly";

    void ChangeAnimationState(string newState) {
        if (currentState == newState) return;
        this.animator.Play(newState);
        currentState = newState;
    }

    void Update() {
        if (this.player.isSpinning && !this.player.isBellying) {
            ChangeAnimationState(PLAYER_SPIN);
            return;
        }
        if (this.player.isOnGround) {
            float speedX = this.rigidbody.velocity.x;
            if (speedX != 0) {
                if (this.player.isSliding) {
                    ChangeAnimationState(PLAYER_SLIDE);
                } else {
                    ChangeAnimationState(PLAYER_RUN);
                }
            } else if (!this.player.isSpinning && !this.player.isBellying && !this.player.isSliding) {
                ChangeAnimationState(PLAYER_IDLE);
            }
        } else {
            float speedY = this.rigidbody.velocity.y;
            if (this.player.isBellying) {
                ChangeAnimationState(PLAYER_BELLY);
            } else {
                if (speedY > 0) {
                    if (this.player.actualJump == this.player.maxJumps) {
                        ChangeAnimationState(PLAYER_DOUBLE_JUMP);
                    } else {
                        ChangeAnimationState(PLAYER_JUMP);
                    }
                } else if (speedY < 0) {
                    ChangeAnimationState(PLAYER_FALL);
                }
            }
        }
    }
}
