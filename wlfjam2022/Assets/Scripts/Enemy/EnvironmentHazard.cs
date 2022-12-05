using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentHazard : MonoBehaviour {

    public GameObject m_particleSystem;
    private bool m_isActive = true;
    private void OnTriggerEnter2D (Collider2D collision) {
        if (m_isActive) {
            if (collision.transform.CompareTag ("Player")) {
                collision.transform.GetComponent<Player> ().GetHit (true);
            }
        }
    }
    public void SetActive (bool active) {
        if (!active && m_particleSystem != null) {
            StartCoroutine (StopParticlesCoroutine ());
        } else if ((m_isActive && !active) && m_particleSystem != null) {
            foreach (var item in m_particleSystem.GetComponentsInChildren<ParticleSystem> ()) {
                item.Play ();
            }
            m_particleSystem.SetActive (true);
        }
        m_isActive = active;

    }
    private IEnumerator StopParticlesCoroutine () {
        ParticleSystem[] particles = m_particleSystem.GetComponentsInChildren<ParticleSystem> ();
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
        m_particleSystem.SetActive (false);
    }
}