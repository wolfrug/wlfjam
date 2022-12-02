using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementState {
    Air,
    Ground
}

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D m_rb;
    private BoxCollider2D m_collider;
    private Vector2 m_movement;
    [SerializeField, Tooltip("Character ground speed")]
    private float m_movementSpeed = 2;
    [SerializeField, Tooltip("How fast the character's movement can be changed midair")]
    private float m_airSpeed = .5f;
    [SerializeField, Tooltip("Jump initial force")]
    private float m_jumpForce = 2;
    [SerializeField, Tooltip("Jump upwards gravity divider")]
    private float m_jumpSpeed = 1;
    [SerializeField, Tooltip("How fast character falls")]
    private float m_fallSpeed = 1;

    private bool m_canJump = false;

    private MovementState m_movementState { 
        get { 
            if (IsGrounded()) {
                m_canJump = true;
                return MovementState.Ground; 
            } 
            else {
                return MovementState.Air;
            }
        }
    }

    private void Awake() {
        m_rb = GetComponent<Rigidbody2D>();
        m_collider = GetComponent<BoxCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (m_movementState) {
            case MovementState.Air:
                HandleAirMovement();
                break;
            case MovementState.Ground:
                HandleGroundMovement();
                break;
            default:
                break;
        }
    }

    private void HandleGroundMovement() {
        m_rb.velocity = new Vector2(m_movement.x * m_movementSpeed, m_rb.velocity.y);
    }

    private void HandleAirMovement() {
        if(m_rb.velocity.y > 0) {
            m_rb.gravityScale = 1 / m_jumpSpeed;
        }
        else {
            m_rb.gravityScale = m_fallSpeed;
        }
        float velocityX = m_rb.velocity.x + m_movement.x * m_airSpeed;
        velocityX = Mathf.Clamp(velocityX, -m_movementSpeed, m_movementSpeed);
        m_rb.velocity = new Vector2(velocityX, m_rb.velocity.y);
    }

    private void HandleJump() {
        if (!m_canJump) {
            return;
        }
        switch (m_movementState) {
            case MovementState.Ground:
                float force = (Mathf.Sqrt(2 * 9.81f * m_jumpForce) * m_rb.mass) / Time.fixedDeltaTime;
                m_rb.AddForce(new Vector2(0, force));
                m_canJump = false;
                break;
        }
    }

    private bool IsGrounded() {
        return Physics2D.BoxCast(transform.position, m_collider.size, 0, Vector2.down, .1f, LayerMask.GetMask("World"));
    }

    private void OnMove(Vector2 value) {
        m_movement = value;
    }

    private void OnJump() {
        HandleJump();
    }

    private void OnEnable() {
        InputEventSender.OnMoveEvent += OnMove;
        InputEventSender.OnJumpEvent += OnJump;
    }

    private void OnDisable() {
        InputEventSender.OnMoveEvent -= OnMove;
        InputEventSender.OnJumpEvent -= OnJump;
    }
}
