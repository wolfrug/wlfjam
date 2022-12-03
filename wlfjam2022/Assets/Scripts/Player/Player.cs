using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool IsHiding { get; private set; }
    public bool IsDancing { get; private set; }

    private Transform m_respawnPoint;

    public void GetHit(bool isEnvironmentHazard = false) {
        if(m_respawnPoint == null) {
            return;
        }
        if (IsHiding && !isEnvironmentHazard) {
            return;
        }
        transform.position = m_respawnPoint.position;
        RequestDanceEnd();
        RequestHideEnd();
    }

    public void HitCheckPoint(Transform transform) {
        m_respawnPoint = transform;
    }

    private void RequestDanceStart() {
        if (IsDancing) {
            return;
        }
        IsDancing = true;
        GlobalEventSender.SendDanceStart();
    }

    private void RequestDanceEnd() {
        if (!IsDancing) {
            return;
        }
        IsDancing = false;
        GlobalEventSender.SendDanceEnd();
    }

    private void RequestHideStart() {
        if (IsHiding) {
            return;
        }
        IsHiding = true;
        GlobalEventSender.SendHideStart();
    }

    private void RequestHideEnd() {
        if (!IsHiding) {
            return;
        }
        IsHiding = false;
        GlobalEventSender.SendHideEnd();
    }

    private void OnEnable() {
        GlobalEventSender.RequestDanceStartEvent += RequestDanceStart;
        GlobalEventSender.RequestDanceEndEvent += RequestDanceEnd;
        GlobalEventSender.RequestHideStart += RequestHideStart;
        GlobalEventSender.RequestHideEnd += RequestHideEnd;
    }

    private void OnDisable() {
        GlobalEventSender.RequestDanceStartEvent -= RequestDanceStart;
        GlobalEventSender.RequestDanceEndEvent -= RequestDanceEnd;
        GlobalEventSender.RequestHideStart -= RequestHideStart;
        GlobalEventSender.RequestHideEnd -= RequestHideEnd;
    }
}
