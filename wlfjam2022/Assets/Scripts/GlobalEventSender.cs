using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalEventSender : MonoBehaviour {

    public delegate void DanceEvent();

    public static DanceEvent OnDanceStart;
    public static DanceEvent OnDanceEnd;

    public static void SendDanceStart() {
        OnDanceStart?.Invoke();
    }

    public static void SendDanceEnd() {
        OnDanceEnd?.Invoke();
    }

    public delegate void HideEvent();
    public static HideEvent OnHideStart;
    public static HideEvent OnHideEnd;

    public static void SendHideStart() {
        OnHideStart?.Invoke();
    }

    public static void SendHideEnd() {
        OnHideEnd?.Invoke();
    }
}
