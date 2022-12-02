using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    private bool m_isDancing = false;

    private void OnDance() {
        m_isDancing = !m_isDancing;
        if (m_isDancing) {
            GlobalEventSender.SendDanceStart();
        }
        else {
            GlobalEventSender.SendDanceEnd();
        }
    }
    private void OnEnable() {
        InputEventSender.OnDance += OnDance;
    }
}
