using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class LightExplosionCameraTrigger : MonoBehaviour {
    public float m_minimumLightLevel = 1f;
    [SerializeField]
    private CinemachineVirtualCamera m_camera;
    [SerializeField]
    private Interactable m_interactable;
    [SerializeField]
    private bool m_activateOnce = true;
    private bool m_isActivated = false;
    private LightMeter m_lightMeter;

    void Start () {
        m_lightMeter = FindObjectOfType<LightMeter> ();
    }
    private void OnTriggerEnter2D (Collider2D collision) {
        if (collision.transform.CompareTag ("Player")) {
            if (m_lightMeter.currentFill >= m_minimumLightLevel) {
                CameraManager.Instance.ChangeCamera (m_camera);
                if (m_interactable != null) {
                    if (m_activateOnce && m_isActivated) {
                        return;
                    }
                    m_interactable.Activate ();
                    m_isActivated = true;
                }
            }
        }
    }
}