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

    [SerializeField]
    private MovementSettingsData m_disabledMovementSettings;
    [SerializeField]
    private GameObject m_danceParticles;

    public GameObject m_deathParticlePrefab;

    private Transform m_respawnPoint;

    private PlayerMovement m_playerMovement;

    private void Start () {
        m_playerMovement = GetComponent<PlayerMovement> ();
        m_playerMovement.SetMovementValues (m_walkMovementSettings);
        m_danceParticles.SetActive (false);
    }

    public void GetHit (bool isEnvironmentHazard = false) {
        if (m_respawnPoint == null) {
            return;
        }
        if (IsHiding && !isEnvironmentHazard) {
            return;
        }
        if (m_deathParticlePrefab != null) {
            Instantiate (m_deathParticlePrefab, transform.position + new Vector3 (0f, 3f, 0f), Quaternion.identity);
        }
        transform.position = m_respawnPoint.position;
        RequestDanceEnd ();
        RequestHideEnd ();
        if (AudioManager.instance != null) {
            AudioManager.instance.PlaySFX ("hit");
        };
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
        StartDanceParticles ();
    }

    private void RequestDanceEnd () {
        if (!IsDancing) {
            return;
        }
        IsDancing = false;
        m_playerMovement.SetMovementValues (m_walkMovementSettings);
        GlobalEventSender.SendDanceEnd ();
        StartCoroutine (StopDanceParticlesCoroutine ());
    }

    private void RequestHideStart () {
        if (IsHiding || IsDancing || !m_playerMovement.IsGrounded ()) {
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

    private void StartDanceParticles () {
        foreach (var item in m_danceParticles.GetComponentsInChildren<ParticleSystem> ()) {
            item.Play ();
        }
        m_danceParticles.SetActive (true);
    }

    private IEnumerator StopDanceParticlesCoroutine () {
        ParticleSystem[] particles = m_danceParticles.GetComponentsInChildren<ParticleSystem> ();
        int particlesCount = 0;
        foreach (var item in particles) {
            item.Stop ();
            particlesCount += item.particleCount;
        }
        while (particlesCount > 0) {
            particlesCount = 0;
            foreach (var item in particles) {
                particlesCount += item.particleCount;
            }
            yield return null;
        }
        m_danceParticles.SetActive (false);
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