using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputEventSender
{
    public delegate void InputEvent();
    public delegate void InputVectorEvent(Vector2 value);

    public static InputVectorEvent OnMoveEvent;
    public static InputEvent OnJumpEvent;

    public static void SendOnMove(Vector2 value) {
        OnMoveEvent?.Invoke(value);
    }

    public static void SendOnJump() { 
        OnJumpEvent?.Invoke();
    }
}
