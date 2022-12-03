using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool IsHiding { get; private set; }
    public bool IsDancing { get; private set; }

    private void OnDanceStart() {
        IsDancing = true;
    }

    private void OnDanceEnd() { 
        IsDancing = false;
    }

    private void OnHideStart() {
        IsHiding = true;
    }

    private void OnHideEnd() {
        IsHiding = false;
    }

    private void OnEnable() {
        GlobalEventSender.OnDanceStart += OnDanceStart;
        GlobalEventSender.OnDanceEnd += OnDanceEnd;
        GlobalEventSender.OnHideStart += OnHideStart;
        GlobalEventSender.OnHideEnd += OnHideEnd;
    }

    private void OnDisable() {
        GlobalEventSender.OnDanceStart -= OnDanceStart;
        GlobalEventSender.OnDanceEnd -= OnDanceEnd;
        GlobalEventSender.OnHideStart -= OnHideStart;
        GlobalEventSender.OnHideEnd -= OnHideEnd;
    }
}
