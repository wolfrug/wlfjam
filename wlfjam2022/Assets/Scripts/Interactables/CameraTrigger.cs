using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera m_camera;
    [SerializeField]
    private Interactable m_interactable;
    [SerializeField]
    private bool m_activateOnce = true;
    private bool m_isActivated = false;
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform.CompareTag("Player")) {
            CameraManager.Instance.ChangeCamera(m_camera);
            if (m_interactable != null) {
                if(m_activateOnce && m_isActivated) {
                    return;
                }
                m_interactable.Activate();
                m_isActivated = true;
            }
        }
    }
}
