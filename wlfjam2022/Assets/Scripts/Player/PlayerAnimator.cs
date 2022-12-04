using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {
    public Animator animator;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start () {
        if (animator == null) {
            animator = GetComponent<Animator> ();
        }
        if (rb == null) {
            rb = GetComponent<Rigidbody2D> ();
        }
        GlobalEventSender.OnDanceStart += DanceStart;
        GlobalEventSender.OnDanceEnd += DanceEnd;
        GlobalEventSender.OnHideStart += HideStart;
        GlobalEventSender.OnHideEnd += HideEnd;
    }

    void DanceStart () {
        animator.SetBool ("isAnimating", true);
    }
    void DanceEnd () {
        animator.SetBool ("isAnimating", false);
    }

    void HideStart () {
        animator.SetBool ("isVeiled", true);
    }
    void HideEnd () {
        animator.SetBool ("isVeiled", false);
    }

    // Update is called once per frame
    void FixedUpdate () {
        animator.SetFloat ("speed", rb.velocity.x);
        animator.SetFloat ("airspeed", rb.velocity.y);
        if (rb.velocity.magnitude > 0f) {
            animator.SetBool ("isMoving", true);
        } else {
            animator.SetBool ("isMoving", false);
        }
        if (rb.velocity.y != 0f) {
            animator.SetBool ("isJumping", true);
        } else {
            if (animator.GetBool ("isJumping")) {
                AudioManager.instance.PlaySFX ("land");
            }
            animator.SetBool ("isJumping", false);
        }
    }
}