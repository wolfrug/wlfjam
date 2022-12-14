using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    private PlayerControls m_playerInputs;

    private void OnEnable() {
        m_playerInputs = new PlayerControls();
        m_playerInputs.Movement.Move.performed += OnMove;
        m_playerInputs.Movement.Move.canceled += OnMove;
        m_playerInputs.Movement.Jump.performed += OnJump;
        m_playerInputs.Movement.Jump.canceled += OnJumpCancel;

        m_playerInputs.Interaction.Dance.performed += OnDance;
        m_playerInputs.Interaction.Dance.canceled += OnDance;
        m_playerInputs.Interaction.Hide.performed += OnHide;
        m_playerInputs.Interaction.Hide.canceled += OnHide;

        m_playerInputs.Enable();
    }

    private void OnDisable() {
        m_playerInputs.Movement.Move.performed -= OnMove;
        m_playerInputs.Movement.Move.canceled -= OnMove;
        m_playerInputs.Movement.Jump.performed -= OnJump;
        m_playerInputs.Movement.Jump.canceled -= OnJumpCancel;

        m_playerInputs.Interaction.Dance.performed -= OnDance;
        m_playerInputs.Interaction.Dance.canceled -= OnDance;
        m_playerInputs.Interaction.Hide.performed -= OnHide;
        m_playerInputs.Interaction.Hide.canceled -= OnHide;
        m_playerInputs.Disable();
    }

    private void OnMove(InputAction.CallbackContext ctx) {
        InputEventSender.SendOnMove(ctx.ReadValue<Vector2>());
    }

    private void OnJump(InputAction.CallbackContext ctx) {
        InputEventSender.SendOnJump();
    }

    private void OnJumpCancel(InputAction.CallbackContext ctx) {
        InputEventSender.SendOnJumpCancel();
    }

    private void OnDance(InputAction.CallbackContext ctx) {
        InputEventSender.SendOnDance();
    }

    private void OnHide(InputAction.CallbackContext ctx) {
        InputEventSender.SendOnHide();
    }
}
