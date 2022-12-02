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
            animator.SetBool ("isJumping", false);
        }
    }
}