using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LightExplosionDancePad : MonoBehaviour {

    public float m_requiredLightLevel = 1f;
    [SerializeField]
    private Interactable m_activateObject;

    [SerializeField]
    private SpriteRenderer m_fillObject;

    public DancePadDoneEvent m_activatedEvent;
    private bool m_isActive;
    private bool m_isDancing = false;
    private bool m_isFull {
        get {
            return m_completionNormalized >= 1;
        }
    }
    private float m_completionNormalized = 0f;
    private Timer m_completionTimer;
    private Timer m_coolDownTimer;
    private LightMeter m_lightMeter;

    private void Start () {
        m_completionTimer = new Timer (5);
        m_coolDownTimer = new Timer (2);
        m_lightMeter = FindObjectOfType<LightMeter> ();
    }

    private void Update () {
        if (m_isFull) {
            return;
        }

        if (!m_isActive && m_completionNormalized == 0) {
            return;
        }

        if (m_isDancing && m_completionNormalized > m_lightMeter.currentFill) {
            if (!m_lightMeter.lightMeterAnimator.GetBool ("bounce")) {
                m_lightMeter.lightMeterAnimator.SetTrigger ("bounce");
            };
            return;
        }

        if (m_isDancing && m_completionTimer.Update ()) {
            m_completionNormalized = 1 - m_completionTimer.timeLeftNormalized;
        } else if (m_isDancing && !m_completionTimer.Update ()) {
            m_completionNormalized = 1;
            if (m_activateObject != null) {
                m_activateObject.Activate ();
                m_activatedEvent.Invoke (null);
            }
        }

        if (!m_isDancing && !m_coolDownTimer.Update ()) {
            m_completionNormalized -= .1f * Time.deltaTime;
            m_completionNormalized = Mathf.Clamp01 (m_completionNormalized);
        }
        // Fill fill
        if (m_fillObject != null) {
            m_fillObject.color = new Color (m_fillObject.color.r, m_fillObject.color.g, m_fillObject.color.b, m_completionNormalized);
        }
    }

    private void OnTriggerEnter2D (Collider2D collision) {
        if (collision.transform.CompareTag ("Player")) {
            m_isActive = true;
            if (collision.transform.GetComponent<Player> ().IsDancing) {
                OnDanceStart ();
            }
        }
    }

    private void OnTriggerExit2D (Collider2D collision) {
        if (collision.transform.CompareTag ("Player")) {
            m_isActive = false;
        }
    }

    private void OnDanceStart () {
        if (!m_isActive) {
            return;
        }
        m_isDancing = true;
        m_completionTimer.Reset ((1 - m_completionNormalized) * m_completionTimer.duration);
        m_coolDownTimer.Pause ();
    }
    private void OnDanceEnd () {
        if (!m_isActive) {
            return;
        }
        m_isDancing = false;
        m_coolDownTimer.Reset ();
        m_completionTimer.Pause ();
    }

    private void OnEnable () {
        GlobalEventSender.OnDanceStart += OnDanceStart;
        GlobalEventSender.OnDanceEnd += OnDanceEnd;
    }

    private void OnDisable () {
        GlobalEventSender.OnDanceStart -= OnDanceStart;
        GlobalEventSender.OnDanceEnd -= OnDanceEnd;
    }
}