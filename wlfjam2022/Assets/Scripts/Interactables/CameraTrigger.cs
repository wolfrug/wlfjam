using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera m_camera;
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform.CompareTag("Player")) {
            CameraManager.Instance.ChangeCamera(m_camera);
        }
    }
}
