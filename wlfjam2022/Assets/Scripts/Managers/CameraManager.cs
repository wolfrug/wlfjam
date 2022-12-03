using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    public CinemachineVirtualCamera CurrentCamera;

    protected override void Awake() {
        base.Awake();
        foreach (var item in FindObjectsOfType<CinemachineVirtualCamera>(true)) {
            if(item.CompareTag("MainCamera")) {
                item.gameObject.SetActive(true);
                CurrentCamera = item;
                continue;
            }
            item.gameObject.SetActive(false);
        }
    }

    public void ChangeCamera(CinemachineVirtualCamera targetCamera) {
        if(CurrentCamera == targetCamera) {
            return;
        }

        CurrentCamera.gameObject.SetActive(false);
        CurrentCamera = targetCamera;
        CurrentCamera.gameObject.SetActive(true);
    }
}
