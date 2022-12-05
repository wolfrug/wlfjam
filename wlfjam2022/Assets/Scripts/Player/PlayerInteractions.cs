using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour {
    private bool m_isDancing = false;
    private bool m_isHiding = false;

    private bool m_canDance = true;
    private bool m_canHide = true;

    public bool CanDance {
        set {
            m_canDance = value;
            if (!value) {
                GlobalEventSender.SendRequestDanceEnd ();
            }
        }
        get {
            return m_canDance;
        }
    }

    public bool CanHide {
        set {
            m_canHide = value;
            if (!value) {
                GlobalEventSender.SendRequestHideEnd ();
            }
        }
        get {
            return m_canHide;
        }
    }

    private void OnDance () {
        if (!m_canDance) {
            return;
        }
        m_isDancing = !m_isDancing;
        if (m_isDancing) {
            GlobalEventSender.SendRequestDanceStart ();
        } else {
            GlobalEventSender.SendRequestDanceEnd ();
        }
    }

    private void OnHide () {
        if (!m_canHide) {
            return;
        }
        m_isHiding = !m_isHiding;
        if (m_isHiding) {
            GlobalEventSender.SendRequestHideStart ();
        } else {
            GlobalEventSender.SendRequestHideEnd ();
        }
    }

    private void OnEnable () {
        InputEventSender.OnDance += OnDance;
        InputEventSender.OnHide += OnHide;
    }

    private void OnDisable () {
        InputEventSender.OnDance -= OnDance;
        InputEventSender.OnHide -= OnHide;
    }
}