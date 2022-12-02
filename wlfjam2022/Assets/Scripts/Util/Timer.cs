using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer {
    public float duration;
    public float timeRemaining;
    public float timeElapsed { get => duration - timeRemaining; }
    public bool isPlaying { get => timeRemaining > 0 && play; }
    private bool play;
    public float timeLeftNormalized {
        get => timeRemaining / duration;
    }

    public Timer() {
        duration = 0;
        Reset();
    }

    public Timer(float duration) {
        this.duration = duration;
        Reset();
    }

    public bool Update() {
        if (!isPlaying) {
            return false;
        }

        timeRemaining -= Time.deltaTime;
        if (!isPlaying) {
            Stop();
            return false;
        }
        return true;
    }

    public void Stop() {
        timeRemaining = 0;
    }

    public void Reset() {
        timeRemaining = duration;
        Start();
    }

    public void Reset(float value) {
        value = Mathf.Min(value, duration);
        timeRemaining = value;
        Start();
    }

    public void Start() {
        play = true;
    }

    public void Pause() {
        play = false;
    }
}
