using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MovementSettings", menuName = "Data/MovementSettings")]
public class MovementSettingsData : ScriptableObject
{
    [Tooltip("Character ground speed")]
    public float MovementSpeed = 2;
    [Tooltip("How fast the character's movement can be changed midair")]
    public float AirSpeed = .5f;
    [Tooltip("Jump initial force")]
    public float JumpForce = 2;
    [Tooltip("Jump upwards gravity divider")]
    public float JumpSpeed = 1;
    [Tooltip("How fast character falls")]
    public float FallSpeed = 1;
    [Tooltip("If the characters jas inertia in start and end of movement")]
    public bool HasInertia = true;
    public float AccelerationSpeed = 1;
    public float DeccelerationSpeed = 1;
    [Tooltip("How fast the character turns mid run")]
    public float TurnSpeed = 1;
    public bool CanJump = true;
}
