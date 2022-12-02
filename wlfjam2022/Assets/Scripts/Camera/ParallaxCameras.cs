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
    public GameObject[] parallaxBackgrounds;
    public float fovDistance = 60f;
}
public class ParallaxCameras : MonoBehaviour {
    public List<ParallaxBackground> m_backgrounds;

    public float m_scaleToFOVRatio = 3f; // how many times larger the fov is to the scale

    // Start is called before the first frame update
    void Start () {
        foreach (ParallaxBackground bck in m_backgrounds) {
            SetupBackground (bck);
        }
    }

    void SetupBackground (ParallaxBackground bck) {
        bck.parallaxCamera.fieldOfView = bck.fovDistance;
        float ratiodScale = bck.fovDistance / m_scaleToFOVRatio;
        foreach (GameObject go in bck.parallaxBackgrounds) {
            go.transform.localScale = new Vector3 (ratiodScale, ratiodScale, ratiodScale);
        }
    }

    // Update is called once per frame
    void Update () {

    }
}