using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    private bool m_isDancing = false;
    private bool m_isHiding = false;

    private void OnDance() {
        m_isDancing = !m_isDancing;
        if (m_isDancing) {
            GlobalEventSender.SendDanceStart();
        }
        else {
            GlobalEventSender.SendDanceEnd();
        }
    }

    private void OnHide() {
        m_isHiding = !m_isHiding;
        if (m_isHiding) {
            GlobalEventSender.SendHideStart();
        }

        else {
            GlobalEventSender.SendHideEnd();
        }
    }

    private void OnEnable() {
        InputEventSender.OnDance += OnDance;
        InputEventSender.OnHide += OnHide;
    }

    private void OnDisable() {
        InputEventSender.OnDance -= OnDance;
        InputEventSender.OnHide -= OnHide;
    }
}
