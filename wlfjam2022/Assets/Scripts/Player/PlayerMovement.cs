using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementState {
    Air = 1000,
    Ground = 2000
}

public class PlayerMovement : MonoBehaviour {
    private Rigidbody2D m_rb;
    private BoxCollider2D m_collider;
    private Vector2 m_movement;
    private float m_movementSpeed = 2;
    private float m_airSpeed = .5f;
    private float m_jumpForce = 2;
    private float m_jumpSpeed = 1;
    private float m_fallSpeed = 1;
    private bool m_hasInertia = true;
    private float m_accelerationSpeed = 1;
    private float m_deccelerationSpeed = 1;
    private float m_turnSpeed = 1;
    private bool m_canJump;

    private bool m_canDance;

    private bool CanJump {
        get {
            return m_rb.velocity.y <= 0 && IsDropJump && m_canJump;
        }
    }
    private bool IsDropJump {
        get {
            return Time.time - m_lastGroundTIme <= m_jumpAfterDropThreshHold;
        }
    }
    private bool m_isJumpCancelled = false;
    private float m_jumpAfterDropThreshHold = .1f;
    private float m_lastGroundTIme;

    private MovementState m_movementState {
        get {
            if (IsGrounded ()) {
                m_isJumpCancelled = false;
                return MovementState.Ground;
            } else {
                return MovementState.Air;
            }
        }
    }

    private void Awake () {
        m_rb = GetComponent<Rigidbody2D> ();
        m_collider = GetComponent<BoxCollider2D> ();
    }

    void FixedUpdate () {
        switch (m_movementState) {
            case MovementState.Air:
                HandleAirMovement ();
                break;
            case MovementState.Ground:
                HandleGroundMovement ();
                break;
            default:
                break;
        }
    }

    public void SetMovementValues (MovementSettingsData data) {
        m_movementSpeed = data.MovementSpeed;
        m_airSpeed = data.AirSpeed;
        m_jumpForce = data.JumpForce;
        m_jumpSpeed = data.JumpSpeed;
        m_fallSpeed = data.FallSpeed;
        m_hasInertia = data.HasInertia;
        m_accelerationSpeed = data.AccelerationSpeed;
        m_deccelerationSpeed = data.DeccelerationSpeed;
        m_turnSpeed = data.TurnSpeed;
        m_canJump = data.CanJump;
        m_canDance = data.CanDance;
    }

    private void HandleGroundMovement () {
        if (m_hasInertia) {
            //acceleration
            int deccMultiplier = m_rb.velocity.x < 0 ? 1 : -1; //direction
            int accMultiplier = m_movement.x > 0 ? 1 : -1; // direction
            float turnSpeed = m_movement.x * m_rb.velocity.x < 0 ? m_turnSpeed : 0; //If the character is turning
            if (m_movement.x != 0) {
                float velocityX = m_rb.velocity.x + (m_accelerationSpeed + turnSpeed) * accMultiplier;
                velocityX = Mathf.Clamp (velocityX, -m_movementSpeed, m_movementSpeed);
                m_rb.velocity = new Vector2 (velocityX, m_rb.velocity.y);
            }

            // direction
            //decceleration right
            else if (m_movement.x == 0 && m_rb.velocity.x > 0) {
                float velocityX = m_rb.velocity.x + m_deccelerationSpeed * deccMultiplier;
                velocityX = Mathf.Clamp (velocityX, 0, m_movementSpeed);
                m_rb.velocity = new Vector2 (velocityX, m_rb.velocity.y);
            }
            //decceleration left
            else if (m_movement.x == 0 && m_rb.velocity.x < 0) {
                float velocityX = m_rb.velocity.x + m_deccelerationSpeed * deccMultiplier;
                velocityX = Mathf.Clamp (velocityX, -m_movementSpeed, 0);
                m_rb.velocity = new Vector2 (velocityX, m_rb.velocity.y);
            }
        } else {
            m_rb.velocity = new Vector2 (m_movement.x * m_movementSpeed, m_rb.velocity.y);
        }
    }

    private void HandleAirMovement () {
        if (m_rb.velocity.y > 0 && !m_isJumpCancelled) {
            m_rb.gravityScale = 1 / m_jumpSpeed;
        } else {
            m_rb.gravityScale = m_fallSpeed;
        }
        float velocityX = m_rb.velocity.x + m_movement.x * m_airSpeed;
        velocityX = Mathf.Clamp (velocityX, -m_movementSpeed, m_movementSpeed);
        m_rb.velocity = new Vector2 (velocityX, m_rb.velocity.y);
    }

    private void HandleJump () {
        if (!CanJump) {
            return;
        }
        float force = (Mathf.Sqrt (2 * 9.81f * m_jumpForce) * m_rb.mass) / Time.fixedDeltaTime;
        if (IsDropJump) {
            m_rb.velocity = new Vector2 (m_rb.velocity.x, 0);
        }
        m_rb.AddForce (new Vector2 (0, force));
    }

    private void HandleJumpCancel () {
        if (!m_canJump) {
            return;
        }
        if (m_rb.velocity.y < 0) {
            return;
        }
        m_isJumpCancelled = true;
        m_rb.velocity = new Vector2 (m_rb.velocity.x, m_rb.velocity.y - (m_rb.velocity.y * .5f));
        m_rb.gravityScale = m_fallSpeed;
    }

    public bool IsGrounded () {
        if (Physics2D.BoxCast (transform.position, m_collider.size, 0, Vector2.down, .1f, LayerMask.GetMask ("World"))) {
            m_lastGroundTIme = Time.time;
            return true;
        }
        return false;
    }

    private void OnMove (Vector2 value) {
        m_movement = value;
    }

    private void OnJump () {
        HandleJump ();
    }

    private void OnJumpCancel () {
        HandleJumpCancel ();
    }

    private void OnEnable () {
        InputEventSender.OnMoveEvent += OnMove;
        InputEventSender.OnJumpEvent += OnJump;
        InputEventSender.OnJumpCancelEvent += OnJumpCancel;
    }

    private void OnDisable () {
        InputEventSender.OnMoveEvent -= OnMove;
        InputEventSender.OnJumpEvent -= OnJump;
        InputEventSender.OnJumpCancelEvent -= OnJumpCancel;
    }
}