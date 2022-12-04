using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMeter : MonoBehaviour {
    public int maxNumberofDancePads = 5;
    public Animator lightMeterAnimator;
    public float currentFill = 0f;
    // Start is called before the first frame update
    void Start () {
        foreach (DancePad pad in FindObjectsOfType<DancePad> ()) {
            pad.m_activatedEvent.AddListener (AddFill);
        }
    }

    public void AddFill (DancePad dancePad) { // Dance pad done!
        float fillToAdd = 1f / (float) maxNumberofDancePads;
        lightMeterAnimator.SetFloat ("light_fill_amount", currentFill + fillToAdd);
        currentFill += fillToAdd;
    }

    // Update is called once per frame
    void Update () {

    }
}