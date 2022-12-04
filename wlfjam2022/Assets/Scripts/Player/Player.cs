using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public bool IsHiding { get; private set; }
    public bool IsDancing { get; private set; }

    [SerializeField]
    private MovementSettingsData m_walkMovementSettings;
    [SerializeField]
    private MovementSettingsData m_hideMovementSettings;
    [SerializeField]
    private MovementSettingsData m_danceMovementSettings;

    private Transform m_respawnPoint;

    private PlayerMovement m_playerMovement;

    private void Start () {
        m_playerMovement = GetComponent<PlayerMovement> ();
        m_playerMovement.SetMovementValues (m_walkMovementSettings);
    }

    public void GetHit (bool isEnvironmentHazard = false) {
        if (m_respawnPoint == null) {
            return;
        }
        if (IsHiding && !isEnvironmentHazard) {
            return;
        }
        transform.position = m_respawnPoint.position;
        RequestDanceEnd ();
        RequestHideEnd ();
        AudioManager.instance.PlaySFX ("hit");
    }

    public void HitCheckPoint (Transform transform) {
        m_respawnPoint = transform;
    }

    private void RequestDanceStart () {
        if (IsDancing || IsHiding) {
            return;
        }
        IsDancing = true;
        m_playerMovement.SetMovementValues (m_danceMovementSettings);
        GlobalEventSender.SendDanceStart ();
    }

    private void RequestDanceEnd () {
        if (!IsDancing) {
            return;
        }
        IsDancing = false;
        m_playerMovement.SetMovementValues (m_walkMovementSettings);
        GlobalEventSender.SendDanceEnd ();
    }

    private void RequestHideStart () {
        if (IsHiding || IsDancing) {
            return;
        }
        IsHiding = true;
        m_playerMovement.SetMovementValues (m_hideMovementSettings);
        GlobalEventSender.SendHideStart ();
    }

    private void RequestHideEnd () {
        if (!IsHiding) {
            return;
        }
        IsHiding = false;
        m_playerMovement.SetMovementValues (m_walkMovementSettings);
        GlobalEventSender.SendHideEnd ();
    }

    private void OnEnable () {
        GlobalEventSender.RequestDanceStartEvent += RequestDanceStart;
        GlobalEventSender.RequestDanceEndEvent += RequestDanceEnd;
        GlobalEventSender.RequestHideStart += RequestHideStart;
        GlobalEventSender.RequestHideEnd += RequestHideEnd;
    }

    private void OnDisable () {
        GlobalEventSender.RequestDanceStartEvent -= RequestDanceStart;
        GlobalEventSender.RequestDanceEndEvent -= RequestDanceEnd;
        GlobalEventSender.RequestHideStart -= RequestHideStart;
        GlobalEventSender.RequestHideEnd -= RequestHideEnd;
    }
}