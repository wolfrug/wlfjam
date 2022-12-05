using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMeter : MonoBehaviour {
    public int maxNumberofDancePads = 5;
    public Animator lightMeterAnimator;
    public float currentFill = 0f;
    private static float currentFillStatic = 0f;
    // Start is called before the first frame update
    void Start () {
        foreach (DancePad pad in FindObjectsOfType<DancePad> ()) {
            pad.m_activatedEvent.AddListener (AddFill);
        }
    }

    public void AddFill (DancePad dancePad) { // Dance pad done!
        float fillToAdd = 1f / (float) maxNumberofDancePads;
        //lightMeterAnimator.SetFloat ("light_fill_amount", currentFill + fillToAdd);
        currentFill += fillToAdd;
        LightFill = currentFill;
    }

    public static float LightFill {
        get {
            return currentFillStatic;
        }
        set {
            currentFillStatic = value;
        }
    }

    // Update is called once per frame
    void Update () {
        float lerpedValue = Mathf.Lerp (lightMeterAnimator.GetFloat ("light_fill_amount"), currentFill, Time.deltaTime);
        lightMeterAnimator.SetFloat ("light_fill_amount", lerpedValue);
    }
}