using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Set up the cameras to render their own layers
// Cameras must be Perspective
// Set their depths so the furthest layer has the smallest depth
// Remove the background (/environment) from all cameras but the last
// Place the background layers normally on top of one another
// Add cameras to player
// Add script to wherever, and set up distances etc.

[System.Serializable]
public class ParallaxBackground {
    public Camera parallaxCamera;
    public string parallaxCameraTag = "ParallaxCamera1";
    public GameObject[] parallaxBackgrounds;
    public float fovDistance = 60f;
}
public class ParallaxCameras : MonoBehaviour {

    public Camera m_mainCamera;
    public List<ParallaxBackground> m_backgrounds;
    public float m_yAdjustment = -32f; // how far down the parallax background object should be placed

    public float m_scaleAdjustment = 0.001f; // to avoid super large objects

    // Start is called before the first frame update
    void Start () {
        if (m_mainCamera == null) {
            m_mainCamera = Camera.main;
        }
        foreach (ParallaxBackground bck in m_backgrounds) {
            SetupBackground (bck);
        }
        transform.position = new Vector3 (transform.position.x, m_yAdjustment, transform.position.z);
    }

    void SetupBackground (ParallaxBackground bck) {
        if (bck.parallaxCamera == null) {
            bck.parallaxCamera = GameObject.FindGameObjectWithTag (bck.parallaxCameraTag).GetComponent<Camera> ();
        }
        //bck.parallaxCamera.fieldOfView = bck.fovDistance;
        bck.parallaxCamera.fieldOfView = m_mainCamera.fieldOfView;
        foreach (GameObject go in bck.parallaxBackgrounds) {
            go.transform.position = new Vector3 (go.transform.position.x, go.transform.position.y, bck.fovDistance);
            float distance = (m_mainCamera.transform.position - go.transform.position).magnitude;
            float size = distance * m_scaleAdjustment * m_mainCamera.fieldOfView;
            go.transform.localScale = Vector3.one * size;
        }

    }

    // Update is called once per frame
    void Update () {

    }
}